using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Commands.Logout
{
     public record LogoutCommand : IRequest<LogoutResponse>;
     public class LogoutCommandHandler :IRequestHandler<LogoutCommand, LogoutResponse>
     {
          private readonly ILogout _logout;

          public LogoutCommandHandler(ILogout logout)
          {
               _logout = logout;
          }

          public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
          {
               LogoutResponse response = new()
               {
                    ValidationErrors = new List<string>()
               };
               var result = await _logout.ApplyAsync();
               if (result is true)
               {
                    response.LogoutIsSuccessful = true;
                    response.Success = true;
               }
               else
               {

                    response.LogoutIsSuccessful = false;
                    response.Success = false;
               }
               return response;
          }
     }
}
