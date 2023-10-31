using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Vehicle.Commands.Update
{
     public record UpdateVehicleCommand(VehicleModel Model) : IRequest<UpdateVehicleResponse>
     {
          public byte? VehicleId = Model.VehicleId;
          public string? VehicleName = Model.VehicleName;
          public byte? SeatNumber = Model.SeatNumber;

     }
     public class EditVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, UpdateVehicleResponse>
    {
         private readonly IUpdateEntity<Domain.Entities.Vehicle> _updateEntity;
        private readonly IGetEntityById<Domain.Entities.Vehicle> _getEntity;

        public EditVehicleCommandHandler( IUpdateEntity<Domain.Entities.Vehicle> updateEntity, IGetEntityById<Domain.Entities.Vehicle> getEntity)
        {
             _updateEntity = updateEntity;
             _getEntity = getEntity;
        }
        public async Task<UpdateVehicleResponse> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
             var validator = new UpdateVehicleCommandValidator();

             UpdateVehicleResponse response = new()
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

             var vehicleToUpdate = await _getEntity.GetByIdAsync(request.VehicleId);

             if (vehicleToUpdate == null)
             {
                  response.Success = false;

                  response.BaseMessage = $"The Vehicle with id {request.VehicleId} was not found";

                  return response;
             }

             vehicleToUpdate.VehicleName = request.VehicleName;
             vehicleToUpdate.SeatNumber = request.SeatNumber;
             var updateResult = await _updateEntity.UpdateAsync(vehicleToUpdate);

             if (!updateResult)
             {
                  response.Success = false;

                  response.BaseMessage = "Update Vehicle database error";

                  return response;
             }

             response.Success = true;

             return response;

          }
    }
}
