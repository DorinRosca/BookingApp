using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Features.Role.Commands.Delete
{
     public record DeleteRoleCommand(string RoleId) : IRequest<DeleteRoleResponse>;

     public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
     {
          private readonly IGetEntityById<IdentityRole> _entityById;
          private readonly IDeleteEntity<IdentityRole> _deleteEntity;

          public DeleteRoleCommandHandler(IDeleteEntity<IdentityRole> deleteEntity, IGetEntityById<IdentityRole> entityById)
          {
               _deleteEntity = deleteEntity;
               _entityById = entityById;
          }

          public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
          {
               var validator = new DeleteRoleCommandValidator();

               DeleteRoleResponse response = new()
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

               var roleToDelete = await _entityById.GetByIdAsync(request.RoleId);

               if (roleToDelete != null && await _deleteEntity.DeleteAsync(roleToDelete))
               {
                    response.DeleteIsSuccessful = true;
                    response.Success = true;

                    return response;
               }
               else
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "Delete Role database error";

                    return response;
               }
          }
     }
}
