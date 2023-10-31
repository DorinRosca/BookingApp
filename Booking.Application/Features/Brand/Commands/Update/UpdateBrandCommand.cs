using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Brand.Commands.Update
{
     public record UpdateBrandCommand(BrandModel Model) : IRequest<UpdateBrandResponse>
     {
          public byte? BrandId = Model.BrandId;
          public string? BrandName = Model.BrandName;
     }

     public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
     {
          private readonly IUpdateEntity<Domain.Entities.Brand> _updateEntity;
          private readonly IGetEntityById<Domain.Entities.Brand> _getEntity;

          public UpdateBrandCommandHandler(IUpdateEntity<Domain.Entities.Brand> updateEntity, IGetEntityById<Domain.Entities.Brand> getEntity)
          {
               _updateEntity = updateEntity;
               _getEntity = getEntity;
          }

          public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
          {
               var validator = new UpdateBrandCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new UpdateBrandResponse
               {
                    Success = true,
                    UpdateIsSuccessful = true,
                    ValidationErrors = new List<string>()
               };

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";
                    response.ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    return response;
               }

               var brandToUpdate = await _getEntity.GetByIdAsync(request.Model.BrandId);

               if (brandToUpdate == null)
               {
                    response.Success = false;
                    response.UpdateIsSuccessful = false;
                    response.BaseMessage = $"The brand with id {request.Model.BrandId} was not found";
                    return response;
               }

               brandToUpdate.BrandName = request.Model.BrandName;

               var updateResult = await _updateEntity.UpdateAsync(brandToUpdate);

               if (!updateResult)
               {
                    response.Success = false;
                    response.BaseMessage = "Update brand database error";
               }

               return response;
          }
     }
}
