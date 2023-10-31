using Booking.Application.Responses;

namespace Booking.Application.Features.Order.Commands.Confirm
{
    public class ConfirmOrderResponse : BaseResponse
    {
        public bool ConfirmIsSuccessful { get; set; }

    }
}
