using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Interfaces
{
     public interface IDrive
     {
          public Task<List<Drive>> GetAllDrives();
          public Task<bool> AddDrive(DriveViewModel model);
          public Task<bool> DeleteDrive(DriveViewModel model);

     }
}
