using Microsoft.EntityFrameworkCore;
using Booking.Application.Contracts.Database.Car;

namespace Booking.Database.Car
{
     public class GetCarById :IGetCarById
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetCarById(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<Domain.Entities.Car?> GetAsync(int? id)
          {
               var ctx = await _factory.CreateDbContextAsync();
                    var car = ctx.Car
                    .Include(c => c.Brand)
                    .Include(c => c.FuelType)
                    .Include(c => c.Transmission)
                    .Include(c => c.Drive)
                    .Include(c => c.Vehicle)
                    .FirstOrDefault(c => c.CarId == id);
                    return car;
          }
     }
}
