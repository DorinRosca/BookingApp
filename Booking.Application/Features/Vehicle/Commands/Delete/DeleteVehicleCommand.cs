using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Vehicle.Commands.Delete
{
     public record DeleteVehicleCommand(byte VehicleId) : IRequest<DeleteVehicleResponse>
     {

          public byte VehicleId = VehicleId;

     }
     public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, DeleteVehicleResponse>
    {
        private readonly IGetEntityById<Domain.Entities.Vehicle> _entityById;
        private readonly IDeleteEntity<Domain.Entities.Vehicle> _deleteEntity;

        public DeleteVehicleCommandHandler( IDeleteEntity<Domain.Entities.Vehicle> deleteEntity, IGetEntityById<Domain.Entities.Vehicle> entityById)
        {
             _deleteEntity = deleteEntity;
             _entityById = entityById;
        }
        public async Task<DeleteVehicleResponse> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
             var validator = new DeleteVehicleCommandValidator();

             DeleteVehicleResponse response = new()
             {
                  ValidationErrors = new List<string>()
             };

             var validationResult = await validator.ValidateAsync(request, cancellationToken);

             if (!validationResult.IsValid)
             {
                  response.Success = false;

                  response.BaseMessage = "There are some validation errors";

                  foreach (var e in validationResult.Errors)
                  {
                       response.ValidationErrors.Add(e.ErrorMessage);
                  }

                  return response;
             }
             var vehicleToDelete = await _entityById.GetByIdAsync(request.VehicleId);

             if (vehicleToDelete != null && await _deleteEntity.DeleteAsync(vehicleToDelete))
             {
                  response.DeleteIsSuccessful = true;

                  return response;
             }
             else
             {
                  response.Success = false;

                  response.BaseMessage = "Delete Vehicle database error";

                  return response;
             }
        }
    }
}
