using CarBookingApp.Features.Users.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Users.Command.SetRole
{
    public record SetRoleCommand :IRequest<bool>
     {
               [Required, StringLength(50)] public string UserName { get; set; }
               [Required, StringLength(50)] public string RoleName { get; set; }

               public SetRoleCommand(UserRoleViewModel model)
               {
                    UserName = model.UserName;
                    RoleName = model.RoleName;
               }
     }
}
