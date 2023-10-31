using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Transmission.Commands.Update
{
    public record UpdateTransmissionCommand(TransmissionModel Model) : IRequest<UpdateTransmissionResponse>
    {
        public byte? TransmissionId = Model.TransmissionId;
        public string? TransmissionName = Model.TransmissionName;

    }
    public class UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, UpdateTransmissionResponse>
    {
        private readonly IUpdateEntity<Domain.Entities.Transmission> _updateEntity;
        private readonly IGetEntityById<Domain.Entities.Transmission> _getEntity;

        public UpdateTransmissionCommandHandler(IUpdateEntity<Domain.Entities.Transmission> updateEntity, IGetEntityById<Domain.Entities.Transmission> getEntity)
        {
            _updateEntity = updateEntity;
            _getEntity = getEntity;
        }
        public async Task<UpdateTransmissionResponse> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTransmissionCommandValidator();

            UpdateTransmissionResponse response = new()
            {
                ValidationErrors = new List<string>()
            };

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.UpdateIsSuccessful = false;
                response.BaseMessage = "There are some validation errors";

                foreach (var e in validationResult.Errors)
                {
                    response.ValidationErrors.Add(e.ErrorMessage);
                }

                return response;
            }

            var transmissionToUpdate = await _getEntity.GetByIdAsync(request.TransmissionId);

            if (transmissionToUpdate == null)
            {
                response.Success = false;
                response.UpdateIsSuccessful = false;
                response.BaseMessage = $"The Transmission with id {request.TransmissionId} was not found";

                return response;
            }

            transmissionToUpdate.TransmissionName = request.TransmissionName;

            var updateResult = await _updateEntity.UpdateAsync(transmissionToUpdate);

            if (!updateResult)
            {
                response.Success = false;

                response.BaseMessage = "Update Transmission database error";

                return response;
            }

            response.Success = true;
            response.UpdateIsSuccessful = true;
            return response;

        }
    }
}
