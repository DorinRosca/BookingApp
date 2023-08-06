using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Roles.Command.Delete
{
    public record DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }

        public DeleteRoleCommand(string Id)
        {
            RoleId = Id;
        }
    }
}
