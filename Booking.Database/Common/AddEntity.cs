using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     public class AddEntity<TEntity> :IAddEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public AddEntity(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> InsertAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync(); 
               await ctx.Set<TEntity>().AddAsync(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
