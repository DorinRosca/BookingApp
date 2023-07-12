using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CarBookingApp.Repositories
{
     public class CarRepository : ICar
     {
          private readonly DataContext _dataContext;
          private readonly IMapper _mapper;

          public CarRepository(DataContext context, IMapper mapper)
          {
               this._dataContext = context;
               _mapper=mapper;
          }
          public CarCreateViewModel GetCarDetails()
          {
               using (var context = _dataContext)
               {
                    var viewModel = new CarCreateViewModel
                    {
                         Brand = context.Brand.ToList(),
                         Drive = context.Drive.ToList(),
                         FuelType = context.FuelType.ToList(),
                         Transmission = context.Transmission.ToList(),
                         Vehicle = context.Vehicle.ToList()
                    };

                    return viewModel;
               }
          }

          public async Task<bool> CreateCar(CarCreateViewModel model)
          {
               if (model is null)
               {
                    return false;
               }


               SetImage(model);
               using(var context = _dataContext)
               {
                    var entity = _mapper.Map<Car>(model);
                    context.Car.Add(entity);
                    var result = await context.SaveChangesAsync();
                    return result > 0;
               }
          }

          public CarCreateViewModel SetImage(CarCreateViewModel model)
          {
               if (model.ImageFile != null && model.ImageFile.Length > 0)
               {
                    using (var memoryStream = new MemoryStream())
                    {
                         model.ImageFile.CopyTo(memoryStream);
                         model.Image = memoryStream.ToArray();
                    }
               }
               return model;
          }
          public CarEditViewModel SetImage(CarEditViewModel model)
          {
               if (model.ImageFile != null && model.ImageFile.Length > 0)
               {
                    using (var memoryStream = new MemoryStream())
                    {
                         model.ImageFile.CopyTo(memoryStream);
                         model.Image = memoryStream.ToArray();
                    }
               }
               return model;
          }
          public async Task<IEnumerable<CarViewModel>> GetAllAvailableCars()
          {
               using (var context = _dataContext)
               {
                    var carList = await context.Car
                         .Include(c => c.Brand)
                         .Include(c => c.FuelType)
                         .Include(c => c.Transmission)
                         .Include(c => c.Drive)
                         .Include(c => c.Vehicle)
                         .ToListAsync();
                    var cars = _mapper.ProjectTo<CarViewModel>(carList.AsQueryable()).ToList();
                    foreach (var item in cars )
                    {
                         item.ImageFile = GetImage(item);
                    }
                    return cars;
               }

          }

          public async Task<CarViewModel> GetSingleCar(int id)
          {
               using (var context = _dataContext)
               {
                    var result = await context.Car
                         .Include(c => c.Brand)
                         .Include(c => c.FuelType)
                         .Include(c => c.Transmission)
                         .Include(c => c.Drive)
                         .Include(c => c.Vehicle)
                         .FirstOrDefaultAsync(c => c.CarId == id);
                    var car = _mapper.Map<CarViewModel>(result);
                    return car;
               }
          }

          public IFormFile GetImage(CarViewModel model)
          {
               using (var memoryStream = new MemoryStream(model.Image))
               {
                    return new FormFile(memoryStream, 0, model.Image.Length, "CarImage", "file");
               }
          }

          public async Task<CarEditViewModel> GetEditCar(int id)
          {
               var model = await _dataContext.Car.FindAsync(id);

               if(model is null)
               {
                    return null;
               }

               var viewmodel = _mapper.Map<CarEditViewModel>(model);
                    
                    viewmodel.Brand= await _dataContext.Brand.ToListAsync();
                    viewmodel.Drive = await _dataContext.Drive.ToListAsync();
                    viewmodel.FuelType = await _dataContext.FuelType.ToListAsync();
                    viewmodel.Transmission = await _dataContext.Transmission.ToListAsync();
                    viewmodel.Vehicle = await _dataContext.Vehicle.ToListAsync();
               return viewmodel;
          }

          public async Task<bool> EditCar(CarEditViewModel viewmodel)
          {
               if (viewmodel.ImageFile is not null)
               {
                    SetImage(viewmodel);
               }
               var model = _mapper.Map<Car>(viewmodel);
               
               // Retrieve the existing car entity from the database
               model.Availability = true;
               _dataContext.Car.Update(model);
               var result = await _dataContext.SaveChangesAsync();
               return result > 0;
          }

          public async Task<bool> DeleteCar(int id)
          {
               if (id == 0)
               {
                    return false;
               }

               var entity = await _dataContext.Car.FindAsync(id);
               if (entity == null)
               {
                    return false;
               }

               _dataContext.Car.Remove(entity);
               var result = await _dataContext.SaveChangesAsync();

               return result > 0;
          }
     }
}
