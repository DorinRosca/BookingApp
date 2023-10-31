using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Booking.Application.Contracts.Database.Car;
using Booking.Application.Contracts.Database.Status;

namespace Booking.Application.Features.Order.Commands.Add;

public record AddOrderCommand(OrderModel Model) : IRequest<AddOrderResponse>
{
     public string? UserId = Model.UserId;
     public int? CarId = Model.CarId;
     public byte? StatusId =Model.StatusId;
     public DateTime RentalStartDate = Model.RentalStartDate;
     public DateTime RentalEndDate = Model.RentalEndDate;
     public double? TotalAmount = Model.TotalAmount;

}
public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, AddOrderResponse>
{
    private readonly IAddEntity<Domain.Entities.Order> _addEntity;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICheckCarAvailability _checkAvailability;
    private readonly IGetCarPrice _carPrice;
    private readonly IGetStatusIdByName _getStatus;
    public AddOrderCommandHandler(IAddEntity<Domain.Entities.Order> addEntity, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICheckCarAvailability checkAvailability, IGetCarPrice carPrice, IGetStatusIdByName getStatus)
    {
         _addEntity = addEntity;
         _mapper = mapper;
         _httpContextAccessor = httpContextAccessor;
         _checkAvailability = checkAvailability;
         _carPrice = carPrice;
         _getStatus = getStatus;
    }
    public async Task<AddOrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddOrderCommandValidator();

        AddOrderResponse response = new()
        {
            ValidationErrors = new List<string>()
        };

        request.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAvailable = await _checkAvailability.CheckAsync(request.CarId, request.RentalStartDate, request.RentalEndDate);
        if (!isAvailable)
        {
             return InsertFailed(response, "The Car Is Not Available");

        }

        request = await GetTotalAmount(request);
        var result = await SetDefaultStatus(request);
        if (result == null)
        {
             return InsertFailed(response, "Set Default Status Error");
        }
        
        var validationResult = await validator.ValidateAsync(result, cancellationToken);

        if (!validationResult.IsValid)
        {
            foreach (var e in validationResult.Errors)
            {
                response.ValidationErrors.Add(e.ErrorMessage);
            }
            return InsertFailed(response, "There are some validation errors");

        }
        var entity = _mapper.Map<Domain.Entities.Order>(result);
        var insertedResponse = await _addEntity.InsertAsync(entity);
        if (!insertedResponse)
        {
            return InsertFailed(response, "Insert Order database error");
        }

        response.Success = true;
        response.AddedIsSuccessful = true;
        response.Order = _mapper.Map<OrderModel>(result);
        return response;
    }
     public async Task<AddOrderCommand?> SetDefaultStatus(AddOrderCommand model)
     {
          var processing = await _getStatus.GetAsync("Processing");
          if (processing == null)
          {
               return null;
          }
          model.StatusId = processing;
          return model;
     }

     public async Task<AddOrderCommand> GetTotalAmount(AddOrderCommand model)
     {
         int daysDifference = (int)(model.RentalEndDate.Date - model.RentalStartDate.Date).TotalDays;
          model.TotalAmount = await _carPrice.GetAsync(model.CarId, daysDifference);
          return model;
     }

     private static AddOrderResponse InsertFailed(AddOrderResponse response, string message)
     {
          response.Success = false;
          response.AddedIsSuccessful = false;
          response.BaseMessage = message;
          return response;
     }
}
