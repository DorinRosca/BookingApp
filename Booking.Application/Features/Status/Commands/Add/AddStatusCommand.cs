using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Status.Commands.Add;

public record AddStatusCommand(StatusModel Model) : IRequest<AddStatusResponse>
{
    public string? StatusName = Model.StatusName;

}
public class AddStatusCommandHandler : IRequestHandler<AddStatusCommand, AddStatusResponse>
{
    private readonly IAddEntity<Domain.Entities.Status> _addEntity;
    public AddStatusCommandHandler(IAddEntity<Domain.Entities.Status> addEntity)
    {
        _addEntity = addEntity;
    }
    public async Task<AddStatusResponse> Handle(AddStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddStatusCommandValidator();

        AddStatusResponse response = new()
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

        var entity = new Domain.Entities.Status()
        {
            StatusName = request.StatusName
        };
        var insertedResponse = await _addEntity.InsertAsync(entity);
        if (!insertedResponse)
        {
            response.Success = false;

            response.BaseMessage = "Insert Status database error";

            return response;
        }

        response.Success = true;
        response.AddedIsSuccessful = true;
        return response;
    }
}
