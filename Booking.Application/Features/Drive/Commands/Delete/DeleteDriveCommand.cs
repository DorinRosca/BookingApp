using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Drive.Commands.Delete
{
     public record DeleteDriveCommand(byte? DriveId) : IRequest<DeleteDriveResponse>;

     public class DeleteDriveCommandHandler : IRequestHandler<DeleteDriveCommand, DeleteDriveResponse>
     {
          private readonly IGetEntityById<Domain.Entities.Drive> _entityById;
          private readonly IDeleteEntity<Domain.Entities.Drive> _deleteEntity;

          public DeleteDriveCommandHandler(IDeleteEntity<Domain.Entities.Drive> deleteEntity, IGetEntityById<Domain.Entities.Drive> entityById)
          {
               _deleteEntity = deleteEntity;
               _entityById = entityById;
          }

          public async Task<DeleteDriveResponse> Handle(DeleteDriveCommand request, CancellationToken cancellationToken)
          {
               var validator = new DeleteDriveCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new DeleteDriveResponse
               {
                    Success = true,
                    DeleteIsSuccessful = true,
                    ValidationErrors = new List<string>()
               };

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";
                    response.ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    return response;
               }

               var driveToDelete = await _entityById.GetByIdAsync(request.DriveId);

               if (driveToDelete != null && await _deleteEntity.DeleteAsync(driveToDelete))
               {
                    response.DeleteIsSuccessful = true;
               }
               else
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "Delete Drive database error";
               }

               return response;
          }
     }
}
