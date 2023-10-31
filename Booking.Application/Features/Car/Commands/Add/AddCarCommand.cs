using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Car.Commands.Add;

public record AddCarCommand(CarModel Model) : IRequest<AddCarResponse>
{

     public string? ModelName = Model.ModelName;
     public short? Year = Model.Year;
     public decimal? PricePerDay = Model.PricePerDay;
     public byte[]? Image = Model.Image;
     public IFormFile? ImageFile = Model.ImageFile;
     public byte? VehicleId = Model.VehicleId;
     public byte? BrandId = Model.BrandId;
     public byte? FuelTypeId = Model.FuelTypeId;
     public byte? TransmissionId = Model.TransmissionId;
     public byte? DriveId = Model.DriveId;


}
public class AddCarCommandHandler : IRequestHandler<AddCarCommand, AddCarResponse>
{
     private readonly IAddEntity<Domain.Entities.Car> _addEntity;
     private readonly IMapper _mapper;
     public AddCarCommandHandler( IAddEntity<Domain.Entities.Car> addEntity, IMapper mapper)
     {
          _addEntity = addEntity;
          _mapper = mapper;
     }
     public async Task<AddCarResponse> Handle(AddCarCommand request, CancellationToken cancellationToken)
     {

          var validator = new AddCarCommandValidator();

          AddCarResponse response = new()
          {
               ValidationErrors = new List<string>()
          };
          var car = await ConvertImageAsync(request);

          if (car == null)
          {
               response.Success = false;
               response.AddedIsSuccessful = false;
               response.BaseMessage = "Image convert to byte error";

               return response;
          }
          var validationResult = await validator.ValidateAsync(car, cancellationToken);
          
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

          var entity = _mapper.Map<Domain.Entities.Car>(car);
          var insertedResponse = await _addEntity.InsertAsync(entity);
          if (!insertedResponse)
          {
               response.Success = false;
               response.AddedIsSuccessful = false;
               response.BaseMessage = "Insert car database error";

               return response;
          }

          response.Success = true;
          response.AddedIsSuccessful = true;
          return response;
     }

     private static async Task<AddCarCommand?> ConvertImageAsync(AddCarCommand request)
     {
          if (request.ImageFile != null && request.ImageFile.Length > 0)
          {
               using var memoryStream = new MemoryStream();
               await request.ImageFile.CopyToAsync(memoryStream);
               request.Image = memoryStream.ToArray();
               return request;
          }
          else
          {
               return null;
          }

     }
}
