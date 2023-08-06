using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.Features.Users.Command.Login
{
     public class LoginUserCommandHandler :IRequestHandler<LoginUserCommand,SignInResult>
     {
          private readonly SignInManager<IdentityUser> _signInManager;

          public LoginUserCommandHandler(SignInManager<IdentityUser> signInManager)
          {
               _signInManager = signInManager;
          }

          public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
          {
               var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);

               return result;
          }
     }
}
