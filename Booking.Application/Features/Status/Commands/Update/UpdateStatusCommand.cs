using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Status.Commands.Update
{
     public record UpdateStatusCommand(StatusModel Model) : IRequest<UpdateStatusResponse>
     {
          public byte? StatusId = Model.StatusId;
          public string? StatusName = Model.StatusName;
     }

     public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, UpdateStatusResponse>
     {
          private readonly IUpdateEntity<Domain.Entities.Status> _updateEntity;
          private readonly IGetEntityById<Domain.Entities.Status> _getEntity;

          public UpdateStatusCommandHandler(IUpdateEntity<Domain.Entities.Status> updateEntity, IGetEntityById<Domain.Entities.Status> getEntity)
          {
               _updateEntity = updateEntity;
               _getEntity = getEntity;
          }

          public async Task<UpdateStatusResponse> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
          {
               var validator = new UpdateStatusCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               if (!validationResult.IsValid)
               {
                    return new UpdateStatusResponse
                    {
                         Success = false,
                         UpdateIsSuccessful = false,
                         BaseMessage = "There are some validation errors",
                         ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
               }

               var statusToUpdate = await _getEntity.GetByIdAsync(request.StatusId);

               if (statusToUpdate == null)
               {
                    return new UpdateStatusResponse
                    {
                         Success = false,
                         UpdateIsSuccessful = false,
                         BaseMessage = $"The Status with id {request.StatusId} was not found"
                    };
               }

               statusToUpdate.StatusName = request.StatusName;

               var updateResult = await _updateEntity.UpdateAsync(statusToUpdate);

               if (!updateResult)
               {
                    return new UpdateStatusResponse
                    {
                         Success = false,
                         BaseMessage = "Update Status database error"
                    };
               }

               return new UpdateStatusResponse
               {
                    Success = true
               };
          }
     }
}
