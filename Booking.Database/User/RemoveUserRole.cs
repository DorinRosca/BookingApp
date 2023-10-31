using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class RemoveUserRole :IRemoveUserRole
     {
          private readonly UserManager<ApplicationUser> _manager;

          public RemoveUserRole(UserManager<ApplicationUser> manager)
          {
               _manager = manager;
          }

          public async Task<bool> RemoveAsync(ApplicationUser user, string roleName)
          {
               var result = await _manager.RemoveFromRoleAsync(user, roleName);
               return result.Succeeded;
          }
     }
}
