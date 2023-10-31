using Booking.Application.Contracts.Database.Car;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Car;

public class GetCarsAsQueryable : IGetCarsAsQueryable
{
     private readonly IDbContextFactory<BookingDbContext> _factory;

     public GetCarsAsQueryable(IDbContextFactory<BookingDbContext> factory)
     {
          _factory = factory;
     }

     public async Task<List<Domain.Entities.Car>> GetAsync()
     {
          await using var ctx = await _factory.CreateDbContextAsync();
          var query =await ctx.Car
               .Include(c => c.Brand)
               .Include(c => c.FuelType)
               .Include(c => c.Transmission)
               .Include(c => c.Drive)
               .Include(c => c.Vehicle)
               .ToListAsync();

          return query; // Return the IQueryable
     }
}