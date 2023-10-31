﻿using Booking.Application.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Common
{
     internal class UpdateEntity<TEntity>:IUpdateEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public UpdateEntity(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> UpdateAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               ctx.Update(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
