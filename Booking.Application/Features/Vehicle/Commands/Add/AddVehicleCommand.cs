using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Vehicle.Commands.Add;

public record AddVehicleCommand(VehicleModel Model) : IRequest<AddVehicleResponse>
{
     public string? VehicleName = Model.VehicleName;
     public byte? SeatNumber = Model.SeatNumber;

}
public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, AddVehicleResponse>
{
     private readonly IAddEntity<Domain.Entities.Vehicle> _addEntity;
     private readonly IMapper _mapper;
     public AddVehicleCommandHandler( IAddEntity<Domain.Entities.Vehicle> addEntity, IMapper mapper)
     {
          _addEntity = addEntity;
          _mapper = mapper;
     }
     public async Task<AddVehicleResponse> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
     {
          var validator = new AddVehicleCommandValidator();

          AddVehicleResponse response = new()
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

          var entity = _mapper.Map<Domain.Entities.Vehicle>(request);

          var insertedResponse = await _addEntity.InsertAsync(entity);

          if (!insertedResponse)
          {
               response.Success = false;
               response.AddedIsSuccessful = false;
               response.BaseMessage = "Insert Vehicle database error";

               return response;
          }

          response.Success = true;
          response.AddedIsSuccessful = true;
          return response;
     }
}
