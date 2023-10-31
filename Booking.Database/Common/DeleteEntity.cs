using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     internal class DeleteEntity<TEntity> :IDeleteEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public DeleteEntity(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> DeleteAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               ctx.Set<TEntity>().Remove(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
