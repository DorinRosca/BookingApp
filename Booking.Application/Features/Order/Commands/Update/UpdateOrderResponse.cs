using Booking.Application.Responses;

namespace Booking.Application.Features.Order.Commands.Update
{
    public class UpdateOrderResponse : BaseResponse
    {
        public bool UpdateIsSuccessful { get; set; }

    }
}
