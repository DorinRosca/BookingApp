using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Car.Commands.Update
{
     public record UpdateCarCommand(CarViewModel? Model) : IRequest<UpdateCarResponse>
     {
          public int? CarId = Model!.CarId;
          public string? ModelName = Model.ModelName;
          public short? Year = Model.Year;
          public decimal? PricePerDay = Model.PricePerDay;
          public byte[]? Image = Model.Image;

          public byte? VehicleId = Model.VehicleId;
          public byte? BrandId = Model.BrandId;
          public byte? FuelTypeId = Model.FuelTypeId;
          public byte? TransmissionId = Model.TransmissionId;
          public byte? DriveId = Model.DriveId;
          public IFormFile? ImageFile = Model.ImageFile;



     }
     public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdateCarResponse>
    {
         private readonly IUpdateEntity<Domain.Entities.Car> _updateEntity;
        private readonly IGetEntityById<Domain.Entities.Car> _getEntity;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler( IUpdateEntity<Domain.Entities.Car> updateEntity, IGetEntityById<Domain.Entities.Car> getEntity, IMapper mapper)
        {
             _updateEntity = updateEntity;
             _getEntity = getEntity;
             _mapper = mapper;
        }
        public async Task<UpdateCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {

             var validator = new UpdateCarCommandValidator();

             UpdateCarResponse response = new()
             {
                  ValidationErrors = new List<string>()
             };
             if (request.ImageFile != null)
             {
                  var car = await ConvertImageAsync(request);

                  if (car == null)
                  {
                       response.Success = false;
                       response.UpdateIsSuccessful = false;
                       response.BaseMessage = "Image convert to byte error";

                       return response;
                  }
             }

             var validationResult = await validator.ValidateAsync(request, cancellationToken);

             if (!validationResult.IsValid)
             {
                  response.Success = false;

                  response.BaseMessage = "There are some validation errors";

                  foreach (var e in validationResult.Errors)
                  {
                       response.ValidationErrors.Add(e.ErrorMessage);
                  }

                  return response;
             }

             var carToUpdate = await _getEntity.GetByIdAsync(request.CarId);

             if (carToUpdate == null)
             {
                  response.Success = false;

                  response.BaseMessage = $"The Car with id {request.CarId} was not found";

                  return response;
             }

             var entity = _mapper.Map<Domain.Entities.Car>(request);

             var updateResult = await _updateEntity.UpdateAsync(entity);

             if (!updateResult)
             {
                  response.Success = false;

                  response.BaseMessage = "Update Car database error";

                  return response;
             }

             response.Success = true;

             return response;

          }
        private static async Task<UpdateCarCommand?> ConvertImageAsync(UpdateCarCommand request)
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
}
