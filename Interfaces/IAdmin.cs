using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarBookingApp.Interfaces
{
     public interface IAdmin
     {
          /*public Task<IEnumerable<TViewModel>> GetAllData<T, TViewModel>()
               where T : class where TViewModel : class;

          public Task<bool> AddData<T, TViewModel>(TViewModel model) where T : class where TViewModel : class;
          public Task<bool> EditData<T, TViewModel>(TViewModel model) where T : class
               where TViewModel : class;


          public Task<bool> DeleteData<T>(byte id) where T : class;
          public Task<TViewModel> GetData<T, TViewModel>(byte id) where T : class
               where TViewModel : class;*/

          public Task<IEnumerable<DriveViewModel>> GetAllDrive();

          public Task<bool> AddDrive(DriveViewModel model);
          public Task<bool>EditDrive(DriveViewModel model);
          public Task<bool> DeleteDrive(byte id);
          public Task<DriveViewModel> GetDrive(byte id);

          public Task<IEnumerable<VehicleViewModel>> GetAllVehicle();

          public Task<bool> AddVehicle(VehicleViewModel model);
          public Task<bool> EditVehicle(VehicleViewModel model);
          public Task<bool> DeleteVehicle(byte id);
          public Task<VehicleViewModel> GetVehicle(byte id);

          public Task<IEnumerable<FuelTypeViewModel>> GetAllFuelType();

          public Task<bool> AddFuelType(FuelTypeViewModel model);
          public Task<bool> EditFuelType(FuelTypeViewModel model);
          public Task<bool> DeleteFuelType(byte id);
          public Task<FuelTypeViewModel> GetFuelType(byte id);

          public Task<IEnumerable<BrandViewModel>> GetAllBrand();

          public Task<bool> AddBrand(BrandViewModel model);
          public Task<bool> EditBrand(BrandViewModel model);
          public Task<bool> DeleteBrand(byte id);
          public Task<BrandViewModel> GetBrand(byte id);

          public Task<IEnumerable<TransmissionViewModel>> GetAllTransmission();

          public Task<bool> AddTransmission(TransmissionViewModel model);
          public Task<bool> EditTransmission(TransmissionViewModel model);
          public Task<bool> DeleteTransmission(byte id);
          public Task<TransmissionViewModel> GetTransmission(byte id);


          public Task<IEnumerable<StatusViewModel>> GetAllStatus();
          public Task<bool> AddStatus(StatusViewModel model);
          public Task<bool> EditStatus(StatusViewModel model);
          public Task<bool> DeleteStatus(byte id);
          public Task<StatusViewModel> GetStatus(byte id);
     }
}
