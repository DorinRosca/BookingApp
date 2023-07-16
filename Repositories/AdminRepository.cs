using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Repositories
{
     public class AdminRepository :IAdmin
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;
          public AdminRepository(DataContext context,IMapper mapper)
          {
               this._context = context;
               this._mapper = mapper;
          }

          public async Task<IEnumerable<TViewModel>> GetAllData<T, TViewModel>() 
               where T : class 
               where TViewModel: class
          {
               var dataList = await _context.Set<T>().ToListAsync();
               var result = dataList.Select(entity => _mapper.Map<TViewModel>(entity));
               return result;
          }
          public async Task<bool> AddData<T, TViewModel>(TViewModel model) 
               where T : class
               where TViewModel : class
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "The model cannot be null.");
               }
               var entity = _mapper.Map<TViewModel, T>(model);
               _context.Set<T>().Add(entity);
               var result = await _context.SaveChangesAsync();
               return result > 0;
          }

          public async Task<bool> EditData<T, TViewModel>(TViewModel model)
               where T : class
               where TViewModel : class
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "The model parameter cannot be null.");
               }

               var entity = _mapper.Map<TViewModel, T>(model);
               _context.Entry(entity).State = EntityState.Modified;

               var result = await _context.SaveChangesAsync();
               return result > 0;
          }


          public async Task<bool> DeleteData<T>(byte id) 
               where T : class
          {
               if (id == 0)
               {
                    throw new ArgumentNullException( nameof(id),"The Id parameter cannot be zero.");
               }

               var entity = await _context.Set<T>().FindAsync(id);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(id),"There is no entity with such Id.");

               }

               _context.Set<T>().Remove(entity);
               
               var result = await _context.SaveChangesAsync();
               return result > 0;
          }
          public async Task<TViewModel> GetData<T, TViewModel>(byte id)
               where T : class
               where TViewModel : class
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id parameter cannot be zero.");

               }

               var entity = await _context.Set<T>().FindAsync(id);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no entity with such Id.");

               }

               var model = _mapper.Map<TViewModel>(entity);
               return model;
          }
          
          //Drive Logic
          public Task<IEnumerable<DriveViewModel>> GetAllDrive()
          {
               return GetAllData<Drive, DriveViewModel>();
          }

          public Task<bool> AddDrive(DriveViewModel model)
          {
               return AddData<Drive, DriveViewModel>(model);
          }

          public Task<bool> EditDrive(DriveViewModel model)
          {
               return EditData<Drive, DriveViewModel>(model);
          }

          public Task<bool> DeleteDrive(byte id)
          {
               return DeleteData<Drive>(id);
          }

          public Task<DriveViewModel> GetDrive(byte id)
          {
               return GetData<Drive, DriveViewModel>(id);
          }
          //Vehicle Logic
          public Task<IEnumerable<VehicleViewModel>> GetAllVehicle()
          {
               return GetAllData<Vehicle, VehicleViewModel>();
          }

          public Task<bool> AddVehicle(VehicleViewModel model)
          {
               return AddData<Vehicle, VehicleViewModel>(model);
          }

          public Task<bool> EditVehicle(VehicleViewModel model)
          {
               return EditData<Vehicle, VehicleViewModel>(model);
          }

          public Task<bool> DeleteVehicle(byte id)
          {
               return DeleteData<Vehicle>(id);
          }

          public Task<VehicleViewModel> GetVehicle(byte id)
          {
               return GetData<Vehicle, VehicleViewModel>(id);
          }

          //FuelType Logic
          public Task<IEnumerable<FuelTypeViewModel>> GetAllFuelType()
          {
               return GetAllData<FuelType, FuelTypeViewModel>();
          }

          public Task<bool> AddFuelType(FuelTypeViewModel model)
          {
               return AddData<FuelType, FuelTypeViewModel>(model);
          }

          public Task<bool> EditFuelType(FuelTypeViewModel model)
          {
               return EditData<FuelType, FuelTypeViewModel>(model);
          }

          public Task<bool> DeleteFuelType(byte id)
          {
               return DeleteData<FuelType>(id);
          }

          public Task<FuelTypeViewModel> GetFuelType(byte id)
          {
               return GetData<FuelType, FuelTypeViewModel>(id);
          }
          //Brand Logic
          public Task<IEnumerable<BrandViewModel>> GetAllBrand()
          {
               return GetAllData<Brand, BrandViewModel>();
          }

          public Task<bool> AddBrand(BrandViewModel model)
          {
               return AddData<Brand, BrandViewModel>(model);
          }

          public Task<bool> EditBrand(BrandViewModel model)
          {
               return EditData<Brand, BrandViewModel>(model);
          }

          public Task<bool> DeleteBrand(byte id)
          {
               return DeleteData<Brand>(id);
          }

          public Task<BrandViewModel> GetBrand(byte id)
          {
               return GetData<Brand, BrandViewModel>(id);
          }

          //Transmission Logic
          public Task<IEnumerable<TransmissionViewModel>> GetAllTransmission()
          {
               return GetAllData<Transmission, TransmissionViewModel>();
          }

          public Task<bool> AddTransmission(TransmissionViewModel model)
          {
               return AddData<Transmission, TransmissionViewModel>(model);
          }

          public Task<bool> EditTransmission(TransmissionViewModel model)
          {
               return EditData<Transmission, TransmissionViewModel>(model);
          }

          public Task<bool> DeleteTransmission(byte id)
          {
               return DeleteData<Transmission>(id);
          }

          public Task<TransmissionViewModel> GetTransmission(byte id)
          {
               return GetData<Transmission, TransmissionViewModel>(id);
          }
          //Status Logic
          public Task<IEnumerable<StatusViewModel>> GetAllStatus()
          {
               return GetAllData<Status, StatusViewModel>();
          }

          public Task<bool> AddStatus(StatusViewModel model)
          {
               return AddData<Status, StatusViewModel>(model);
          }

          public Task<bool> EditStatus(StatusViewModel model)
          {
               return EditData<Status, StatusViewModel>(model);
          }

          public Task<bool> DeleteStatus(byte id)
          {
               return DeleteData<Status>(id);
          }

          public Task<StatusViewModel> GetStatus(byte id)
          {
               return GetData<Status, StatusViewModel>(id);
          }
     
}
}
