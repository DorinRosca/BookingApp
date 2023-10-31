using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Brand.Commands.Add
{
     public record AddBrandCommand(BrandModel Model) : IRequest<AddBrandResponse>
     {
          public string? BrandName = Model.BrandName;
     }

     public class AddBrandCommandHandler : IRequestHandler<AddBrandCommand, AddBrandResponse>
     {
          private readonly IAddEntity<Domain.Entities.Brand> _addEntity;

          public AddBrandCommandHandler(IAddEntity<Domain.Entities.Brand> addEntity)
          {
               _addEntity = addEntity;
          }

          public async Task<AddBrandResponse> Handle(AddBrandCommand request, CancellationToken cancellationToken)
          {
               var validator = new AddBrandCommandValidator();
               var validationResult = await validator.ValidateAsync(request, cancellationToken);

               var response = new AddBrandResponse
               {
                    Success = true,
                    AddedIsSuccessful = true,
                    ValidationErrors = new List<string>()
               };

               if (!validationResult.IsValid)
               {
                    response.Success = false;
                    response.AddedIsSuccessful = false;
                    response.BaseMessage = "There are some validation errors";
                    response.ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    return response;
               }

               var entity = new Domain.Entities.Brand
               {
                    BrandName = request.BrandName
               };

               var insertedResponse = await _addEntity.InsertAsync(entity);

               if (!insertedResponse)
               {
                    response.Success = false;
                    response.BaseMessage = "Insert brand database error";
               }

               return response;
          }
     }
}