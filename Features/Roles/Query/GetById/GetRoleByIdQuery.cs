using CarBookingApp.Features.Roles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Roles.Query.GetById
{
    public record GetRoleByIdQuery : IRequest<RoleViewModel>
    {
        public string RoleId { get; set; }

        public GetRoleByIdQuery(string Id)
        {
            RoleId = Id;
        }
    }
}
