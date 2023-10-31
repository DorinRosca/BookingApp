using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Booking.Database.User
{
     internal class Register :IRegister
     {
          private readonly UserManager<ApplicationUser> _userManager;

          public Register(UserManager<ApplicationUser> manager)
          {
               _userManager = manager;
          }
          

          public async Task<IdentityResult> ApplyAsync(string email,  string password)
          {
               var user = new ApplicationUser()
               {
                    UserName = email,
                    Email = email,
               };
               var result = await _userManager.CreateAsync(user, password);
               return result;
          }
     }
}
