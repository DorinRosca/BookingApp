using Booking.Application.Contracts.Database.Status;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.Status
{
     internal class GetStatusIdByName:IGetStatusIdByName
     {
          private readonly IDbContextFactory<BookingDbContext> _factory;

          public GetStatusIdByName(IDbContextFactory<BookingDbContext> factory)
          {
               _factory = factory;
          }


          public async Task<byte?> GetAsync(string name)
          {
               var ctx = await _factory.CreateDbContextAsync();
               var status = await ctx.Status.FirstOrDefaultAsync(s => s.StatusName == name);

               if (status != null)
               {
                    return status.StatusId;
               }
               else
               {
                    return null;
               }
          }
     }
}
