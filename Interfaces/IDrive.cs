using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Interfaces
{
     public interface IDrive
     {
          public Task<List<DriveViewModel>> GetAllDrives();
          public Task<bool> AddDrive(DriveViewModel model);
          public Task<bool> EditDrive(DriveViewModel model);
          public Task<bool> DeleteDrive(int id);
          public Task<DriveViewModel> GetDrive(int id);

     }
}
