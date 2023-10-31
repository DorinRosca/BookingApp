using Booking.Application.Contracts.Database.User;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database.User
{
     internal class GetUsers :IGetUsers
     {
          private readonly UserManager<ApplicationUser> _manager;

          public GetUsers(UserManager<ApplicationUser> manager)
          {
               _manager = manager;
          }

          public async Task<List<ApplicationUser>> GetAsync()
          {
               return await _manager.Users.ToListAsync();
          }
     }
}
