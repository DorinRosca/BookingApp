using Booking.Application.Responses;

namespace Booking.Application.Features.Status.Commands.Update
{
    public class UpdateStatusResponse : BaseResponse
    {
        public bool UpdateIsSuccessful { get; set; }

    }
}
