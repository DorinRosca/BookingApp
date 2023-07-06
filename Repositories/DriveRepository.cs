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

          public async Task<List<Drive>> GetAllDrives()
          {
               return await _context.Drive.ToListAsync();
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
          public async Task<bool> DeleteDrive(DriveViewModel model)
          {
               if (model is null)
               {
                    return await Task.FromResult(false);
               }

               var drive = new Drive
               {
                    DriveId = model.DriveId
               };
               _context.Drive.Add(drive);
               var result = await _context.SaveChangesAsync();
               return result > 0;
          }
     }
}
