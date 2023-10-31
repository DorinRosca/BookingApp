using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Queries
{
     public record GetUserRoleQuery : IRequest<IEnumerable<UserRoleModel>>;
     public class GetUserRoleQueryHandler :IRequestHandler<GetUserRoleQuery, IEnumerable<UserRoleModel>>
     {
          private readonly IGetUsers _getUsers;
          private readonly  IGetUserRoles _getUserRoles;

          public GetUserRoleQueryHandler(IGetUsers getUsers, IGetUserRoles getUserRoles)
          {
               _getUsers = getUsers;
               _getUserRoles = getUserRoles;
          }

          public async  Task<IEnumerable<UserRoleModel>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
          {
               var userRole = new List<UserRoleModel>();
               var users = await _getUsers.GetAsync();
               if (users.Any())
               {
                    foreach (var user in users)
                    {
                         var roles = await _getUserRoles.GetAsync(user);
                         if (roles is null)
                         {
                              userRole.Add(new UserRoleModel { UserName = user.UserName });
                         }
                         else
                         {
                              userRole.AddRange(roles.Select(role => new UserRoleModel() { UserName = user.UserName, RoleName = role }));
                         }
                    }
               }
               return userRole;
          }
     }
}
