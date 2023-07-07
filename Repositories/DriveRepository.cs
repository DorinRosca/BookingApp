using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Repositories
{
     public class DriveRepository :IDrive
     {
          private readonly DataContext _context;

          public DriveRepository(DataContext context)
          {
               _context = context;
          }

          public async Task<List<DriveViewModel>> GetAllDrives()
          {
               var result = await _context.Drive.ToListAsync();
               var drives = new List<DriveViewModel>();
               foreach (var item in result)
               {
                    drives.Add(new DriveViewModel
                    {
                         DriveId = item.DriveId,
                         DriveName = item.DriveName
                    });
               }

               return drives;
          }
          public async Task<bool> AddDrive(DriveViewModel model)
          {
               if (model is null)
               {
                    return await Task.FromResult(false);
               }
               var drive = new Drive
               {
                    DriveName = model.DriveName
               };
               _context.Drive.Add(drive);
               var result = await _context.SaveChangesAsync(); 
               return result > 0;
          }

          public async Task<bool> EditDrive(DriveViewModel model)
          {
               if (model is null)
               {
                    return await Task.FromResult(false);
                    ;
               }

               Drive data = new Drive
               {
                    DriveId = model.DriveId,
                    DriveName = model.DriveName
               };
                _context.Drive.Update(data);
                var result = await _context.SaveChangesAsync();
                return result > 0;
          }

          public async Task<bool> DeleteDrive(int id)
          {
               if (id == 0)
               {
                    return false;
               }

               var drive = await _context.Drive.FindAsync(id);
               if (drive == null)
               {
                    return false;
               }

               _context.Drive.Remove(drive);
               var result = await _context.SaveChangesAsync();

               return result > 0;
          }

          public Task<DriveViewModel> GetDrive(int id)
          {
               if (id == 0)
               {
                    return null;
               }

               var model = _context.Drive.FirstOrDefault(d => d.DriveId == id);

               if (model == null)
               {
                    return null;
               }

               // Map the retrieved entity to a DriveViewModel object
               var driveViewModel = new DriveViewModel
               {
                    DriveId = model.DriveId,
                    DriveName = model.DriveName,
                    // Set other properties accordingly
               };

               return Task.FromResult(driveViewModel);
          }

     }
}
