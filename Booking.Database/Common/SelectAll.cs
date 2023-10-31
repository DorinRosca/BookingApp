using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     internal class SelectAll<TEntity> : ISelectAll<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public SelectAll(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IEnumerable<TEntity>> GetDataAsync()
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               return await ctx.Set<TEntity>().ToListAsync();
          }
     }
}
