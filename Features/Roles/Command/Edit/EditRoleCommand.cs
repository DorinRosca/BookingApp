using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Roles.ViewModel;

namespace CarBookingApp.Features.Roles.Command.Edit
{
    public class EditRoleCommand : IRequest<bool>
    {
         public string Id { get; set; }
         [StringLength(50)]

         public string NormalizedName { get; set; }
         [Required, StringLength(50)]
         public string Name { get; set; }

          public EditRoleCommand(RoleViewModel model)
        { 
             Id = model.Id;
            Name = model.Name;
            NormalizedName = model.NormalizedName;
        }
    }
}
