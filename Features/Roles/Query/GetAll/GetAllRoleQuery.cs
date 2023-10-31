using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Roles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Roles.Query.GetAll
{
    public record GetAllRoleQuery : IRequest<IEnumerable<RoleViewModel>>;
}
