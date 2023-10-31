using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Brand.Commands.Delete
{
     public record DeleteBrandCommand(byte? BrandId) : IRequest<DeleteBrandResponse>
     {
          public byte? BrandId = BrandId;
     }

     public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
     {
          private readonly IGetEntityById<Domain.Entities.Brand> _entityById;
          private readonly IDeleteEntity<Domain.Entities.Brand> _deleteEntity;

          public DeleteBrandCommandHandler(IDeleteEntity<Domain.Entities.Brand> deleteEntity, IGetEntityById<Domain.Entities.Brand> entityById)
          {
               _deleteEntity = deleteEntity;
               _entityById = entityById;
          }

          public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
          {
               var validator = new DeleteBrandCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new DeleteBrandResponse
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

               var brandToDelete = await _entityById.GetByIdAsync(request.BrandId);

               if (brandToDelete != null && await _deleteEntity.DeleteAsync(brandToDelete))
               {
                    response.DeleteIsSuccessful = true;
               }
               else
               {
                    response.Success = false;
                    response.DeleteIsSuccessful = false;
                    response.BaseMessage = "Delete brand database error";
               }

               return response;
          }
     }
}
