using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

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
          public async Task<CarCreateViewModel> GetCarDetails()
          {
               var viewModel = new CarCreateViewModel
               {
                    Brand = await _dataContext.Brand.ToListAsync(),
                    Drive = await _dataContext.Drive.ToListAsync(),
                    FuelType = await _dataContext.FuelType.ToListAsync(),
                    Transmission = await _dataContext.Transmission.ToListAsync(),
                    Vehicle = await _dataContext.Vehicle.ToListAsync()
               };
               
               return viewModel;
          }

          public async Task<bool> CreateCar(CarCreateViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "The model parameter cannot be null");
               }

               model = SetImage(model);
               var entity = _mapper.Map<Car>(model);
             
               _dataContext.Car.Add(entity);
               var result = await _dataContext.SaveChangesAsync();
               return result > 0;
          }

          public CarCreateViewModel SetImage(CarCreateViewModel model)
          {
               if (model.ImageFile != null && model.ImageFile.Length > 0)
               {
                    using(var memoryStream = new MemoryStream())
                    {
                         model.ImageFile.CopyTo(memoryStream);
                         model.Image = memoryStream.ToArray();
                    }
                    return model;
               }
               throw new Exception("Cannot Convert Image");
          }
          public CarEditViewModel SetImage(CarEditViewModel model)
          {
               if (model.ImageFile != null && model.ImageFile.Length > 0)
               {
                    using (var memoryStream = new MemoryStream())
                    {
                         model.ImageFile.CopyTo(memoryStream);
                         model.Image= memoryStream.ToArray();
                    }                        
                    return model;

               }

               throw  new Exception("Cannot Convert Image");
          }
          public async Task<CarFilterViewModel> GetFilteredCarList(CarFilterViewModel model)
          {
               using (var context = _dataContext)
               {
                    var query = context.Car
                         .Include(c => c.Brand)
                         .Include(c => c.FuelType)
                         .Include(c => c.Transmission)
                         .Include(c => c.Drive)
                         .Include(c => c.Vehicle)
                         .AsQueryable();
                    if (model != null)
                    {
                         if (!model.SelectedBrandId.IsNullOrEmpty())
                         {
                              query = query.Where(c => model.SelectedBrandId.Contains(c.BrandId));
                         }

                         if (!model.SelectedVehicleId.IsNullOrEmpty())
                         {
                              query = query.Where(c => model.SelectedVehicleId.Contains(c.VehicleId));
                         }

                         if (!string.IsNullOrEmpty(model.SearchCarName))
                         {
                              query = query.Where(c => c.ModelName.Contains(model.SearchCarName));
                         }
                    }

                    var carList = await query.ToListAsync();
                    CarFilterViewModel cars = new CarFilterViewModel();
                    cars.Cars= _mapper.Map<List<CarViewModel>>(carList);

                    cars.Brands = await context.Brand.ToListAsync();
                    cars.Vehicles = await context.Vehicle.ToListAsync();

                    return cars;
               }
          }

          public async Task<CarViewModel> GetSingleCar(int id)
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be zero");
               }
               var result = await _dataContext.Car
                    .Include(c => c.Brand)
                    .Include(c => c.FuelType)
                    .Include(c => c.Transmission)
                    .Include(c => c.Drive)
                    .Include(c => c.Vehicle)
                    .FirstOrDefaultAsync(c => c.CarId == id);
               var car = _mapper.Map<CarViewModel>(result);
               return car;
          }

          
          public async Task<CarEditViewModel> GetEditCar(int id)
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be zero");
               }
               var model = await _dataContext.Car.FindAsync(id);

               if (model == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no entity with such Id");
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
               if (viewmodel == null)
               {
                    throw new ArgumentNullException(nameof(viewmodel), "There model cannot be null");
               }
                              
               if (viewmodel.ImageFile != null)
               {
                    viewmodel = SetImage(viewmodel);

               }
               var model = _mapper.Map<Car>(viewmodel);

               _dataContext.Car.Update(model);
               var result = await _dataContext.SaveChangesAsync();
               return result > 0;
          }

          public async Task<bool> DeleteCar(int id)
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be zero");
               }

               var entity = await _dataContext.Car.FindAsync(id);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no entity with such Id");
               }

               _dataContext.Car.Remove(entity);
               var result = await _dataContext.SaveChangesAsync();

               return result > 0;
          }
     }
}
