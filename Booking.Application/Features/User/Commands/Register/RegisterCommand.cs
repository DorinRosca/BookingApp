using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Commands.Register
{
    public record RegisterCommand(RegisterModel Model) : IRequest<RegisterResponse>
    {
         public string? Email = Model.Email;
         public string? Password = Model.Password;
         public string? ConfirmPassword  = Model.ConfirmPassword;
    }
    public class RegisterCommandHandler :IRequestHandler<RegisterCommand,RegisterResponse>
    {
         private readonly IRegister _register;
         private readonly ILogin _login;

         public RegisterCommandHandler(IRegister register, ILogin login)
         {
              _register = register;
              _login = login;
         }

         public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
         {
              var validator = new RegisterCommandValidator();

              RegisterResponse response = new()
              {
                   ValidationErrors = new List<string>()
              };
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

              if (!validationResult.IsValid)
              {
                   response.Success = false;
                   response.RegisterIsSuccessful = false;
                   response.BaseMessage = "There are some validation errors";

                   foreach (var e in validationResult.Errors)
                   {
                        response.ValidationErrors.Add(e.ErrorMessage);
                   }

                   return response;
              }

              if (request.Email != null && request.ConfirmPassword != null)
              {
                   var result = await  _register.ApplyAsync(request.Email, request.ConfirmPassword);
                   if (!result.Succeeded)
                   {
                        response.Success = false;

                        response.BaseMessage = "Register database error";

                        return response;
                   }
                   response.RegisterIsSuccessful = true;
                   var logInResult = await _login.ApplyAsync(request.Email, request.ConfirmPassword, false);
                   if (logInResult.Succeeded)
                   {
                        response.RegisterIsSuccessful = true;
                        response.LoginIsSuccessful = true;
                        return response;
                   }
              }
              response.LoginIsSuccessful = false;
              response.BaseMessage = "Login database error";
              return response;
          }
    }
}
