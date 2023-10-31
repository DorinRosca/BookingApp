using CarBookingApp.Features.Users.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Users.Query.GetUserRole
{
    public record GetUserRoleQuery :IRequest<IEnumerable<UserRoleViewModel>>
    {
    }
}
