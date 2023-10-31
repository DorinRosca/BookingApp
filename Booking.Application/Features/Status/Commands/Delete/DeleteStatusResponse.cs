using Booking.Application.Responses;

namespace Booking.Application.Features.Status.Commands.Delete
{
    public class DeleteStatusResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
