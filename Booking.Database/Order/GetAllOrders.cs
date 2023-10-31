using Booking.Application.Contracts.Database.Order;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Order
{
     internal class GetAllOrders:IGetAllOrders
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetAllOrders(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }


          public async Task<IEnumerable<Domain.Entities.Order>> GetAsync()
          {
               var ctx = await _factory.CreateDbContextAsync();
               var orders = ctx.Order
                    .Include(o => o.Car)
                    .Include(o => o.User)
                    .Include(o => o.Status);
               return await orders.ToListAsync();
          }
     }
}
