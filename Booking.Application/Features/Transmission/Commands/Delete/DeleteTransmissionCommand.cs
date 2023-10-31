using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Transmission.Commands.Delete
{
    public record DeleteTransmissionCommand(byte? TransmissionId) : IRequest<DeleteUserRoleResponse>
    {

        public byte? TransmissionId = TransmissionId;

    }
    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, DeleteUserRoleResponse>
    {
        private readonly IGetEntityById<Domain.Entities.Transmission> _entityById;
        private readonly IDeleteEntity<Domain.Entities.Transmission> _deleteEntity;

        public DeleteTransmissionCommandHandler(IDeleteEntity<Domain.Entities.Transmission> deleteEntity, IGetEntityById<Domain.Entities.Transmission> entityById)
        {
            _deleteEntity = deleteEntity;
            _entityById = entityById;
        }
        public async Task<DeleteUserRoleResponse> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTransmissionRoleCommandValidator();

            DeleteUserRoleResponse response = new()
            {
                ValidationErrors = new List<string>()
            };

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.DeleteIsSuccessful = false;
                response.BaseMessage = "There are some validation errors";

                foreach (var e in validationResult.Errors)
                {
                    response.ValidationErrors.Add(e.ErrorMessage);
                }

                return response;
            }
            var transmissionToDelete = await _entityById.GetByIdAsync(request.TransmissionId);

            if (transmissionToDelete != null && await _deleteEntity.DeleteAsync(transmissionToDelete))
            {
                response.DeleteIsSuccessful = true;
                response.Success = true;

                return response;
            }
            else
            {
                response.Success = false;
                response.DeleteIsSuccessful = false;
                response.BaseMessage = "Delete Transmission database error";

                return response;
            }
        }
    }
}
