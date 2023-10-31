using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.FuelType.Commands.Update
{
     public record UpdateFuelTypeCommand(FuelTypeModel Model) : IRequest<UpdateFuelTypeResponse>
     {
          public byte? FuelTypeId = Model.FuelTypeId;
          public string? FuelTypeName = Model.FuelTypeName;
     }

     public class UpdateFuelTypeCommandHandler : IRequestHandler<UpdateFuelTypeCommand, UpdateFuelTypeResponse>
     {
          private readonly IUpdateEntity<Domain.Entities.FuelType> _updateEntity;
          private readonly IGetEntityById<Domain.Entities.FuelType> _getEntity;

          public UpdateFuelTypeCommandHandler(IUpdateEntity<Domain.Entities.FuelType> updateEntity, IGetEntityById<Domain.Entities.FuelType> getEntity)
          {
               _updateEntity = updateEntity;
               _getEntity = getEntity;
          }

          public async Task<UpdateFuelTypeResponse> Handle(UpdateFuelTypeCommand request, CancellationToken cancellationToken)
          {
               var validator = new UpdateFuelTypeCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new UpdateFuelTypeResponse
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

               var fuelTypeToUpdate = await _getEntity.GetByIdAsync(request.FuelTypeId);

               if (fuelTypeToUpdate == null)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = $"The FuelType with id {request.FuelTypeId} was not found";
                    return response;
               }

               fuelTypeToUpdate.FuelTypeName = request.FuelTypeName;

               var updateResult = await _updateEntity.UpdateAsync(fuelTypeToUpdate);

               if (!updateResult)
               {
                    response.Success = false;
                    response.BaseMessage = "Update FuelType database error";
               }

               return response;
          }
     }
}
