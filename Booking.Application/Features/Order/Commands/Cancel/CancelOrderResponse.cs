using Booking.Application.Responses;

namespace Booking.Application.Features.Order.Commands.Cancel
{
    public class CancelOrderResponse : BaseResponse
    {
        public bool CancelIsSuccessful { get; set; }

    }
}
