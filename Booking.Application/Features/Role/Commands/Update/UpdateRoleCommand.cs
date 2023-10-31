using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Features.Role.Commands.Update
{
     public record UpdateRoleCommand(RoleModel Model) : IRequest<UpdateRoleResponse>
     {
          public string? RoleId = Model.Id;
          public string? RoleName = Model.Name;
     }

     public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
     {
          private readonly IUpdateEntity<IdentityRole> _updateEntity;
          private readonly IGetEntityById<IdentityRole> _getEntity;

          public UpdateRoleCommandHandler(IUpdateEntity<IdentityRole> updateEntity, IGetEntityById<IdentityRole> getEntity)
          {
               _updateEntity = updateEntity;
               _getEntity = getEntity;
          }

          public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
          {
               var validator = new UpdateRoleCommandValidator();

               UpdateRoleResponse response = new()
               {
                    ValidationErrors = new List<string>()
               };

               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";

                    foreach (var e in validationResult.Errors)
                    {
                         response.ValidationErrors.Add(e.ErrorMessage);
                    }

                    return response;
               }

               var roleToUpdate = await _getEntity.GetByIdAsync(request.RoleId);

               if (roleToUpdate == null)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = $"The Role with id {request.RoleId} was not found";

                    return response;
               }

               roleToUpdate.Name = request.RoleName;
               roleToUpdate.NormalizedName = request.RoleName; // Ensure NormalizedName is updated if needed

               var updateResult = await _updateEntity.UpdateAsync(roleToUpdate);

               if (!updateResult)
               {
                    response.Success = false;
                    response.BaseMessage = "Update Role database error";

                    return response;
               }

               response.Success = true;

               return response;
          }
     }
}
