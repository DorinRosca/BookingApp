using MediatR;

namespace CarBookingApp.Features.Users.Command.Logout
{
     public record LogoutUserCommand : IRequest<bool>;
}
