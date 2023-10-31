using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Drive.Commands.Add
{
     public record AddDriveCommand(DriveModel Model) : IRequest<AddDriveResponse>
     {
          public string? DriveName = Model.DriveName;

     }

     public class AddDriveCommandHandler : IRequestHandler<AddDriveCommand, AddDriveResponse>
     {
          private readonly IAddEntity<Domain.Entities.Drive> _addEntity;

          public AddDriveCommandHandler(IAddEntity<Domain.Entities.Drive> addEntity)
          {
               _addEntity = addEntity;
          }

          public async Task<AddDriveResponse> Handle(AddDriveCommand request, CancellationToken cancellationToken)
          {
               var validator = new AddDriveCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new AddDriveResponse
               {
                    Success = true,
                    AddedIsSuccessful = true,
                    ValidationErrors = new List<string>()
               };

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.AddedIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";
                    response.ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    return response;
               }

               var entity = new Domain.Entities.Drive
               {
                    DriveName = request.Model.DriveName
               };

               var insertedResponse = await _addEntity.InsertAsync(entity);
               if (!insertedResponse)
               {
                    response.Success = false;
                    response.BaseMessage = "Insert Drive database error";
               }

               return response;
          }
     }
}