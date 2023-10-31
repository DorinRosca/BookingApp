using Microsoft.EntityFrameworkCore;
using Booking.Application.Contracts.Database.Car;

namespace Booking.Database.Car
{
     public class CheckCarAvailability :ICheckCarAvailability
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public CheckCarAvailability(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

         

          public async Task<bool> CheckAsync(int? id, DateTime? starTime, DateTime? endTime)
          {
               var cmd =await  _factory.CreateDbContextAsync();
               var cancelledStatusId = await cmd.Status.Where(status => status.StatusName == "cancelled")
                    .Select(status => status.StatusId)
                    .FirstOrDefaultAsync();
               var isAvailable = await cmd.Order
                    .AnyAsync(order =>
                         order.CarId == id &&
                         (
                              (order.RentalStartDate <= starTime && order.RentalEndDate >= starTime) ||  // Order starts within the rental period
                              (order.RentalStartDate <= endTime && order.RentalEndDate >= endTime) ||      // Order ends within the rental period
                              (order.RentalStartDate >= starTime && order.RentalEndDate <= endTime)      // Order completely overlaps the rental period
                         ) &&
                         order.StatusId != cancelledStatusId
                    );

               return !isAvailable;
          }
     }
}
