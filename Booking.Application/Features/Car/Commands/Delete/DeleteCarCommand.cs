using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Car.Commands.Delete
{
     public record DeleteCarCommand(int CarId) : IRequest<DeleteCarResponse>
     {

          public int CarId = CarId;

     }
     public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeleteCarResponse>
    {
        private readonly IGetEntityById<Domain.Entities.Car> _entityById;
        private readonly IDeleteEntity<Domain.Entities.Car> _deleteEntity;

        public DeleteCarCommandHandler( IDeleteEntity<Domain.Entities.Car> deleteEntity, IGetEntityById<Domain.Entities.Car> entityById)
        {
             _deleteEntity = deleteEntity;
             _entityById = entityById;
        }
        public async Task<DeleteCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
             var validator = new DeleteCarCommandValidator();

             DeleteCarResponse response = new()
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
             var carToDelete = await _entityById.GetByIdAsync(request.CarId);

             if (carToDelete != null && await _deleteEntity.DeleteAsync(carToDelete))
             {
                  response.DeleteIsSuccessful = true;
                  response.Success = true;
                  return response;
             }
             else
             {
                  response.Success = false;
                  response.Success = false;
                  response.BaseMessage = "Delete Car database error";

                  return response;
             }
        }
    }
}
