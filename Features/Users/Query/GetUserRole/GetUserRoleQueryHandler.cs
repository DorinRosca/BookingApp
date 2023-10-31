using CarBookingApp.Features.Users.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Users.Query.GetUserRole
{
    public class GetUserRoleQueryHandler :IRequestHandler<GetUserRoleQuery, IEnumerable<UserRoleViewModel>>
     {
          private readonly UserManager<IdentityUser> _userManager;

          public GetUserRoleQueryHandler(UserManager<IdentityUser> userManager)
          {
               _userManager = userManager;
          }

          public async Task<IEnumerable<UserRoleViewModel>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
          {
               var users = await _userManager.Users.ToListAsync(cancellationToken);
               var viewModels = new List<UserRoleViewModel>();

               foreach (var user in users)
               {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count == 0)
                    {
                         var viewModel = new UserRoleViewModel
                         {
                              UserName = user.UserName,
                         };

                         viewModels.Add(viewModel);
                    }
                    else
                    {
                         foreach (var role in roles)
                         {
                              var viewModel = new UserRoleViewModel
                              {
                                   UserName = user.UserName,
                                   RoleName = role
                              };

                              viewModels.Add(viewModel);
                         }
                    }
               }

               return viewModels;
          }
     }
}
