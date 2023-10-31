using Booking.Application.Contracts.Database.User;
using MediatR;

namespace Booking.Application.Features.User.Commands.DeleteRole
{
     public record DeleteUserRoleCommand(UserRoleModel Model) : IRequest<DeleteUserRoleResponse>
     {
          public string? UserName = Model.UserName;
          public string? RoleName = Model.RoleName;

     }

     public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, DeleteUserRoleResponse>
     {
          private readonly IGetUserByName _userByName;
          private readonly IRemoveUserRole _removeUserRole;
          public DeleteUserRoleCommandHandler(IGetUserByName userById, IRemoveUserRole removeUserRole)
          {
               _userByName = userById;
               _removeUserRole = removeUserRole;
          }

          public async Task<DeleteUserRoleResponse> Handle(DeleteUserRoleCommand request,
               CancellationToken cancellationToken)
          {
               var validator = new DeleteUserRoleCommandValidator();

               DeleteUserRoleResponse response = new()
               {
                    ValidationErrors = new List<string>()
               };

               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
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

                    if (user != null && await _removeUserRole.RemoveAsync(user, request.RoleName))
                    {
                         response.DeleteIsSuccessful = true;
                         response.Success = true;

                         return response;
                    }
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "Delete User Role Database Error";

               }
               response.Success = false;
               response.DeleteIsSuccessful = false;
               response.BaseMessage = "Invalid User Role Data";
               return response;

          }
     }


}
