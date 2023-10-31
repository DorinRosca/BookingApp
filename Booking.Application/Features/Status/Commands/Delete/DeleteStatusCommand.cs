using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Status.Commands.Delete
{
     public record DeleteStatusCommand(byte? StatusId) : IRequest<DeleteStatusResponse>
     {
          public byte? StatusId = StatusId;
     }
public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, DeleteStatusResponse>
     {
          private readonly IGetEntityById<Domain.Entities.Status> _entityById;
          private readonly IDeleteEntity<Domain.Entities.Status> _deleteEntity;

          public DeleteStatusCommandHandler(IDeleteEntity<Domain.Entities.Status> deleteEntity, IGetEntityById<Domain.Entities.Status> entityById)
          {
               _deleteEntity = deleteEntity;
               _entityById = entityById;
          }

          public async Task<DeleteStatusResponse> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
          {
               var validator = new DeleteStatusCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    return new DeleteStatusResponse
                    {
                         Success = false,
                         DeleteIsSuccessful = false,
                         BaseMessage = "There are some validation errors",
                         ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
               }

               var statusToDelete = await _entityById.GetByIdAsync(request.StatusId);

               if (statusToDelete != null && await _deleteEntity.DeleteAsync(statusToDelete))
               {
                    return new DeleteStatusResponse
                    {
                         Success = true,
                         DeleteIsSuccessful = true
                    };
               }
               else
               {
                    return new DeleteStatusResponse
                    {
                         Success = false,
                         DeleteIsSuccessful = false,
                         BaseMessage = "Delete Status database error"
                    };
               }
          }
     }
}
