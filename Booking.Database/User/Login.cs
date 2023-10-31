using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class Login :ILogin
     {
          private readonly SignInManager<ApplicationUser> _manager;
          

          public Login(SignInManager<ApplicationUser> manager)
          {
               _manager = manager;
          }

          public async Task<SignInResult> ApplyAsync(string email, string password, bool rememberMe)
          {
               var result = await _manager.PasswordSignInAsync(email, password, rememberMe, false);
               return result;
          }
     }
}
