using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.FuelType.Commands.Delete
{
     public record DeleteFuelTypeCommand(byte? FuelTypeId) : IRequest<DeleteFuelTypeResponse>
     {
          public byte? FuelTypeId = FuelTypeId;
     }

     public class DeleteFuelTypeCommandHandler : IRequestHandler<DeleteFuelTypeCommand, DeleteFuelTypeResponse>
     {
          private readonly IGetEntityById<Domain.Entities.FuelType> _entityById;
          private readonly IDeleteEntity<Domain.Entities.FuelType> _deleteEntity;

          public DeleteFuelTypeCommandHandler(IDeleteEntity<Domain.Entities.FuelType> deleteEntity, IGetEntityById<Domain.Entities.FuelType> entityById)
          {
               _deleteEntity = deleteEntity;
               _entityById = entityById;
          }

          public async Task<DeleteFuelTypeResponse> Handle(DeleteFuelTypeCommand request, CancellationToken cancellationToken)
          {
               var validator = new DeleteFuelTypeCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new DeleteFuelTypeResponse
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

               var fuelTypeToDelete = await _entityById.GetByIdAsync(request.FuelTypeId);

               if (fuelTypeToDelete != null && await _deleteEntity.DeleteAsync(fuelTypeToDelete))
               {
                    response.DeleteIsSuccessful = true;
               }
               else
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "Delete FuelType database error";
               }

               return response;
          }
     }
}
