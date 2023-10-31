using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     internal class SelectAllAsQueryable<TEntity>:ISelectAllAsQueryable<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public SelectAllAsQueryable(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IQueryable<TEntity>> GetAllAsQueryable()
          {
               await using var ctx =await _factory.CreateDbContextAsync();
               return ctx.Set<TEntity>().AsQueryable();
          }
     }
}
