using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Commands.Login
{
    public record LoginCommand(LoginModel Model) : IRequest<LoginResponse>
    {
         public string Email = Model.Email ?? string.Empty;
         public string Password = Model.Password ?? string.Empty;
         public bool RememberMe = Model.RememberMe;
    }
    public class LoginCommandHandler :IRequestHandler<LoginCommand,LoginResponse>
    {
         private readonly ILogin _login;

         public LoginCommandHandler(ILogin login)
         {
              _login = login;
         }

         public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
         {
              var validator = new LoginCommandValidator();

              LoginResponse response = new()
              {
                   ValidationErrors = new List<string>()
              };
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

              if (!validationResult.IsValid)
              {
                   response.Success = false;
                   response.LoginIsSuccessful = false;
                   response.BaseMessage = "There are some validation errors";

                   foreach (var e in validationResult.Errors)
                   {
                        response.ValidationErrors.Add(e.ErrorMessage);
                   }

                   return response;
              }
              var result = await _login.ApplyAsync(request.Email, request.Password, request.RememberMe);
              if (!result.Succeeded)
              {
                   response.Success = false;

                   response.BaseMessage = "Wrong username or password";

                   return response;
              }

              response.Success = true;
              response.LoginIsSuccessful = true;
              return response;
          }
    }
}
