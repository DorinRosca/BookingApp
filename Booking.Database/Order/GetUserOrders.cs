using Microsoft.EntityFrameworkCore;
using Booking.Application.Contracts.Database.Order;

namespace Booking.Database.Order
{
     internal class GetUserOrders :IGetUserOrders
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetUserOrders(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }


          public async Task<IEnumerable<Domain.Entities.Order>> GetAsync(string id)
          {
               var ctx = await _factory.CreateDbContextAsync();
               var orders = ctx.Order
                    .Include(o => o.Car)
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .Where(c => c.UserId != null && c.UserId.Contains(id));
               return await orders.ToListAsync();
          }
     }
}
