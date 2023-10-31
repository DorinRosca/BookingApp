using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class GetUserRoles :IGetUserRoles
     {
          private readonly UserManager<ApplicationUser> _manager;

          public GetUserRoles(UserManager<ApplicationUser> manager)
          {
               _manager = manager;
          }
          public async Task<IList<string>?> GetAsync(ApplicationUser user)
          {
               var roles = await _manager.GetRolesAsync(user);
               return roles;
          }
     }
}
