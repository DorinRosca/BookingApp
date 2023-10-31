using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     internal class GetEntityById<TEntity>:IGetEntityById<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetEntityById(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<TEntity?> GetByIdAsync<T>(T id)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               return await ctx.FindAsync<TEntity>(id);
          }
     }
}
