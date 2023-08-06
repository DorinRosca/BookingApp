using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.Features.Users.Command.Register
{
    public class RegisterUserCommandHandler :IRequestHandler<RegisterUserCommand, IdentityResult>
    {
         private readonly UserManager<IdentityUser> _userManager;
         private readonly SignInManager<IdentityUser> _signInManager;

         public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
         {
              _userManager = userManager;
              _signInManager = signInManager;
         }

         public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
         {
              if (request == null)
              {
                   throw new ArgumentNullException(nameof(request), "There is no such model");

              }

              var user = new IdentityUser
              {
                   UserName = request.Email,
                   Email = request.Email,
              };

              var result = await _userManager.CreateAsync(user, request.Password);
              if (result.Succeeded)
              {
                   await _signInManager.SignInAsync(user, isPersistent: false);

              }
              return result;
          }
    }
}
