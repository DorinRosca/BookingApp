using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.FuelType.Commands.Add
{
     public record AddFuelTypeCommand(FuelTypeModel Model) : IRequest<AddFuelTypeResponse>
     {
          public string? FuelTypeName = Model.FuelTypeName;

     }

     public class AddFuelTypeCommandHandler : IRequestHandler<AddFuelTypeCommand, AddFuelTypeResponse>
     {
          private readonly IAddEntity<Domain.Entities.FuelType> _addEntity;

          public AddFuelTypeCommandHandler(IAddEntity<Domain.Entities.FuelType> addEntity)
          {
               _addEntity = addEntity;
          }

          public async Task<AddFuelTypeResponse> Handle(AddFuelTypeCommand request, CancellationToken cancellationToken)
          {
               var validator = new AddFuelTypeCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new AddFuelTypeResponse
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

               var entity = new Domain.Entities.FuelType
               {
                    FuelTypeName = request.FuelTypeName
               };

               var insertedResponse = await _addEntity.InsertAsync(entity);

               if (!insertedResponse)
               {
                    response.Success = false;
                    response.BaseMessage = "Insert FuelType database error";
               }

               return response;
          }
     }
}