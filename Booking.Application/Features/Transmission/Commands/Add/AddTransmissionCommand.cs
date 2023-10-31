using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Transmission.Commands.Add;

public record AddTransmissionCommand(TransmissionModel Model) : IRequest<AddTransmissionResponse>
{
    public string? TransmissionName = Model.TransmissionName;

}
public class AddTransmissionCommandHandler : IRequestHandler<AddTransmissionCommand, AddTransmissionResponse>
{
    private readonly IAddEntity<Domain.Entities.Transmission> _addEntity;
    public AddTransmissionCommandHandler(IAddEntity<Domain.Entities.Transmission> addEntity)
    {
        _addEntity = addEntity;
    }
    public async Task<AddTransmissionResponse> Handle(AddTransmissionCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddTransmissionCommandValidator();

        AddTransmissionResponse response = new()
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

        var entity = new Domain.Entities.Transmission()
        {
            TransmissionName = request.TransmissionName
        };
        var insertedResponse = await _addEntity.InsertAsync(entity);
        if (!insertedResponse)
        {
            response.Success = false;

            response.BaseMessage = "Insert Transmission" +
                    " database error";

            return response;
        }

        response.Success = true;
        response.AddedIsSuccessful = true;
        return response;
    }
}
