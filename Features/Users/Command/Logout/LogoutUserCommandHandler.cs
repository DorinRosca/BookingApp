using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.Features.Users.Command.Logout
{
     public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
     {
          private readonly SignInManager<IdentityUser> _signInManager;

          public LogoutUserCommandHandler(SignInManager<IdentityUser> signInManager)
          {
               _signInManager = signInManager;
          }

          public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
          {
               await _signInManager.SignOutAsync();
               return true;
          }
     }
}
