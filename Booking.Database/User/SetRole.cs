using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class SetRole :ISetRole
     {
          private readonly UserManager<ApplicationUser> _manager;

          public SetRole(UserManager<ApplicationUser> manager)
          {
               _manager = manager;
          }
         
          public async Task<bool> SetAsync(ApplicationUser user, string roleName)
          {
               var result = await _manager.AddToRoleAsync(user, roleName);
               return result.Succeeded;
          }
     }
}
