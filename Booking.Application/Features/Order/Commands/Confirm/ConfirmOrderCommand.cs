using Booking.Application.Contracts.Database;
using Booking.Application.Contracts.Database.Status;
using MediatR;

namespace Booking.Application.Features.Order.Commands.Confirm
{
    public record ConfirmOrderCommand(int? OrderId) : IRequest<ConfirmOrderResponse>
    {
        public int? OrderId = OrderId;
        public string NewStatus = "Confirmed";
    }
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ConfirmOrderResponse>
    {
        private readonly IUpdateEntity<Domain.Entities.Order> _updateOrder;
        private readonly IGetEntityById<Domain.Entities.Order> _getOrder;
        private readonly IGetStatusIdByName _getStatusId;

        public ConfirmOrderCommandHandler(IUpdateEntity<Domain.Entities.Order> updateOrder, IGetEntityById<Domain.Entities.Order> getOrder, IGetStatusIdByName getStatus)
        {
             _updateOrder = updateOrder;
             _getOrder = getOrder;
             _getStatusId = getStatus;
        }
        public async Task<ConfirmOrderResponse> Handle(ConfirmOrderCommand request, CancellationToken confirmlationToken)
        {
            var validator = new ConfirmOrderCommandValidator();

            ConfirmOrderResponse response = new()
            {
                ValidationErrors = new List<string>()
            };

            var validationResult = await validator.ValidateAsync(request, confirmlationToken);

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

            var orderToUpdate = await _getOrder.GetByIdAsync(request.OrderId);

            if (orderToUpdate == null)
            {
                response.Success = false;

                response.BaseMessage = $"The Order with id {request.OrderId} was not found";

                return response;
            }

            var statusId = await _getStatusId.GetAsync(request.NewStatus);
            if (statusId == null)
            {
                 response.Success = false;

                 response.BaseMessage = $"The status with name {request.NewStatus} was not found";

                 return response;
            }
            orderToUpdate.StatusId = statusId;

               var updateResult = await _updateOrder.UpdateAsync(orderToUpdate);

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
