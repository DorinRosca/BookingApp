using Booking.Application.Responses;

namespace Booking.Application.Features.Order.Commands.Add
{
    public class AddOrderResponse : BaseResponse
    {
        public bool AddedIsSuccessful { get; set; }
        public OrderModel? Order { get; set; }

    }
}
