using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class GetUserByName :IGetUserByName
     {
          private readonly UserManager<ApplicationUser> _manager;

          public GetUserByName(UserManager<ApplicationUser> manager)
          {
               _manager = manager;
          }
          public async Task<ApplicationUser?> GetAsync(string name)
          {
               return await _manager.FindByNameAsync(name);
          }
     }
}
