using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Features.Role.Commands.Add
{
     public record AddRoleCommand(RoleModel Model) : IRequest<AddRoleResponse>
     {
          public string? RoleName = Model.Name;
     }

     public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AddRoleResponse>
     {
          private readonly IAddEntity<IdentityRole> _addEntity;

          public AddRoleCommandHandler(IAddEntity<IdentityRole> addEntity)
          {
               _addEntity = addEntity;
          }

          public async Task<AddRoleResponse> Handle(AddRoleCommand request, CancellationToken cancellationToken)
          {
               var validator = new AddRoleCommandValidator();

               AddRoleResponse response = new()
               {
                    ValidationErrors = new List<string>()
               };

               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.AddedIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";

                    foreach (var e in validationResult.Errors)
                    {
                         response.ValidationErrors.Add(e.ErrorMessage);
                    }

                    return response;
               }

               var entity = new IdentityRole()
               {
                    Name = request.RoleName,
                    NormalizedName = request.RoleName
               };
               var insertedResponse = await _addEntity.InsertAsync(entity);
               if (!insertedResponse)
               {
                    response.Success = false;
                    response.BaseMessage = "Insert Role database error";

                    return response;
               }

               response.Success = true;
               response.AddedIsSuccessful = true;
               return response;
          }
     }
}
