using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Commands.SetRole
{
     public record SetUserRoleCommand(UserRoleModel Model) : IRequest<SetUserRoleResponse>
     {
          public string? UserName = Model.UserName;
          public string? RoleName = Model.RoleName;

     }

     public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, SetUserRoleResponse>
     {
          private readonly IGetUserByName _userByName;
          private readonly ISetRole _setUserRole;
          public SetUserRoleCommandHandler(IGetUserByName userById, ISetRole setRole)
          {
               _userByName = userById;
               _setUserRole = setRole;
          }

          public async Task<SetUserRoleResponse> Handle(SetUserRoleCommand request,
               CancellationToken cancellationToken)
          {
               var validator = new SetUserRoleCommandValidator();

               SetUserRoleResponse response = new()
               {
                    ValidationErrors = new List<string>()
               };

               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.SetIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";

                    foreach (var e in validationResult.Errors)
                    {
                         response.ValidationErrors.Add(e.ErrorMessage);
                    }

                    return response;
               }

               if (request.UserName != null && request.RoleName != null)
               {
                    var user = await _userByName.GetAsync(request.UserName);

                    if (user != null && await _setUserRole.SetAsync(user, request.RoleName))
                    {
                         response.SetIsSuccessful = true;
                         response.Success = true;

                         return response;
                    }
                    else
                    {
                         response.Success = false;
                         response.SetIsSuccessful = false;
                         response.BaseMessage = "Set user role database error";

                         return response;
                    }
               }
               response.Success = false;
               response.SetIsSuccessful = false;
               response.BaseMessage = "Set user role database error";

               return response;
          }
     }


}
