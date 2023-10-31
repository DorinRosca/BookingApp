using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Order.Commands.Update
{
    public record UpdateOrderCommand(OrderModel Model) : IRequest<UpdateOrderResponse>
    {
        public int? OrderId = Model.OrderId;
        public string? UserId = Model.UserId;
        public int? CarId = Model.CarId;
        public byte? StatusId = Model.StatusId;
        public DateTime? RentalStartDate = Model.RentalStartDate;
        public DateTime? RentalEndDate = Model.RentalEndDate;
        public double? TotalAmount = Model.TotalAmount;


    }
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResponse>
    {
        private readonly IUpdateEntity<Domain.Entities.Order> _updateEntity;
        private readonly IGetEntityById<Domain.Entities.Order> _getEntity;

        public UpdateOrderCommandHandler(IUpdateEntity<Domain.Entities.Order> updateEntity, IGetEntityById<Domain.Entities.Order> getEntity)
        {
            _updateEntity = updateEntity;
            _getEntity = getEntity;
        }
        public async Task<UpdateOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderCommandValidator();

            UpdateOrderResponse response = new()
            {
                ValidationErrors = new List<string>()
            };

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

            var orderToUpdate = await _getEntity.GetByIdAsync(request.OrderId);

            if (orderToUpdate == null)
            {
                response.Success = false;

                response.BaseMessage = $"The Order with id {request.OrderId} was not found";

                return response;
            }

            orderToUpdate.UserId = request.UserId;
            orderToUpdate.CarId = request.CarId;
            orderToUpdate.StatusId = request.StatusId;
            orderToUpdate.RentalStartDate = request.RentalStartDate;
            orderToUpdate.RentalEndDate = request.RentalEndDate;
            orderToUpdate.TotalAmount = request.TotalAmount;


            var updateResult = await _updateEntity.UpdateAsync(orderToUpdate);

            if (!updateResult)
            {
                response.Success = false;

                response.BaseMessage = "Update Order database error";

                return response;
            }

            response.Success = true;

            return response;

        }
    }
}
