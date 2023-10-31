using Microsoft.EntityFrameworkCore;
using Booking.Application.Contracts.Database.Car;

namespace Booking.Database.Car
{
     public class GetCarPrice :IGetCarPrice
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetCarPrice(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

         

         

          public async Task<double?> GetAsync(int? id, int daysDiff)
          {
               var cmd = await _factory.CreateDbContextAsync();
               var car = await cmd.Car.FirstOrDefaultAsync(c => c.CarId == id);
               if (car == null)
               {
                    return null;
               }

               var result = car.PricePerDay * daysDiff;
               return (double)result;
          }
     }
}
