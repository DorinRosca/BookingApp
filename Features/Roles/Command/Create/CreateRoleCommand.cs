using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Roles.ViewModel;

namespace CarBookingApp.Features.Roles.Command.Create
{
    public record CreateRoleCommand : IRequest<bool>
    {

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public CreateRoleCommand(RoleViewModel model)
        {
            Name = model.Name;
            NormalizedName = model.NormalizedName;
        }
    }
}
