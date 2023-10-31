using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Drive.Commands.Update
{
     public record UpdateDriveCommand(DriveModel Model) : IRequest<UpdateDriveResponse>
     {
          public byte? DriveId = Model.DriveId;
          public string? DriveName = Model.DriveName;

     }


     public class UpdateDriveCommandHandler : IRequestHandler<UpdateDriveCommand, UpdateDriveResponse>
     {
          private readonly IUpdateEntity<Domain.Entities.Drive> _updateEntity;
          private readonly IGetEntityById<Domain.Entities.Drive> _getEntity;

          public UpdateDriveCommandHandler(IUpdateEntity<Domain.Entities.Drive> updateEntity, IGetEntityById<Domain.Entities.Drive> getEntity)
          {
               _updateEntity = updateEntity;
               _getEntity = getEntity;
          }

          public async Task<UpdateDriveResponse> Handle(UpdateDriveCommand request, CancellationToken cancellationToken)
          {
               var validator = new UpdateDriveCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new UpdateDriveResponse
               {
                    Success = true,
                    UpdateIsSuccessful = true,
                    ValidationErrors = new List<string>()
               };

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";
                    response.ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    return response;
               }

               var driveToUpdate = await _getEntity.GetByIdAsync(request.DriveId);

               if (driveToUpdate == null)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = $"The Drive with id {request.DriveId} was not found";
                    return response;
               }

               driveToUpdate.DriveName = request.DriveName;

               var updateResult = await _updateEntity.UpdateAsync(driveToUpdate);

               if (updateResult) return response;
               response.Success = false;
               response.BaseMessage = "Update Drive database error";

               return response;
          }
     }
}
