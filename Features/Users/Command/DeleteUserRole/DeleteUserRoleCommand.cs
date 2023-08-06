using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Users.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Users.Command.DeleteUserRole
{
    public record DeleteUserRoleCommand : IRequest<bool>
     {
          [Required, StringLength(50)] public string UserName { get; set; }
          [Required, StringLength(50)] public string RoleName { get; set; }

          public DeleteUserRoleCommand(UserRoleViewModel model)
          {
               UserName = model.UserName;
               RoleName = model.RoleName;
          }
     }
}
