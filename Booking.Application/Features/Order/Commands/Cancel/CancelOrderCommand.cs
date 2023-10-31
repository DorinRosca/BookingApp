using Booking.Application.Contracts.Database;
using Booking.Application.Contracts.Database.Status;
using MediatR;

namespace Booking.Application.Features.Order.Commands.Cancel
{
    public record CancelOrderCommand(int? OrderId) : IRequest<CancelOrderResponse>
    {
        public int? OrderId = OrderId;
        public string NewStatus = "Cancelled";
    }
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, CancelOrderResponse>
    {
        private readonly IUpdateEntity<Domain.Entities.Order> _updateOrder;
        private readonly IGetEntityById<Domain.Entities.Order> _getOrder;
        private readonly IGetStatusIdByName _getStatusId;

        public CancelOrderCommandHandler(IUpdateEntity<Domain.Entities.Order> updateOrder, IGetEntityById<Domain.Entities.Order> getOrder, IGetStatusIdByName getStatus)
        {
             _updateOrder = updateOrder;
             _getOrder = getOrder;
             _getStatusId = getStatus;
        }
        public async Task<CancelOrderResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelOrderCommandValidator();

            CancelOrderResponse response = new()
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
