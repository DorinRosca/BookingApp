using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class Logout :ILogout
     {
          private readonly SignInManager<ApplicationUser> _manager;

          public Logout(SignInManager<ApplicationUser> manager)
          {
               _manager = manager;
          }
          public async Task<bool?> ApplyAsync()
          {
               await _manager.SignOutAsync();
               return true;
          }
     }
}
