using CarBookingApp.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.Features.Users.Command.DeleteUserRole
{
     public class DeleteUserRoleCommandHandler :IRequestHandler<DeleteUserRoleCommand, bool>
     {
          private readonly DataContext _dataContext;
          private readonly UserManager<IdentityUser> _userManager;
          private readonly RoleManager<IdentityRole> _roleManager;

          public DeleteUserRoleCommandHandler(DataContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
          {
               _dataContext = context;
               this._userManager = userManager;
               _roleManager = roleManager;
          }
          public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
          {
               if (request == null)
               {
                    throw new ArgumentNullException(nameof(request), "There is no such model");

               }
               var roleId = GetRoleId(request.RoleName).Result;
               var userId = GetUserId(request.UserName).Result;
               if (roleId != null && userId != null)
               {
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
                    return result.Succeeded;
               }

               return false;
          }
          public async Task<string> GetUserId(string name)
          {
               var result = await _userManager.FindByNameAsync(name);
               if (result is null)
               {
                    return null;
               }
               return result.Id;
          }
          public async Task<string> GetRoleId(string name)
          {
               var result = await _roleManager.FindByNameAsync(name);

               if (result is null)
                    return null;
               return result.Id;
          }
     }
}
