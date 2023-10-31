using Booking.Application.Responses;

namespace Booking.Application.Features.Status.Commands.Add
{
    public class AddStatusResponse : BaseResponse
    {
        public bool AddedIsSuccessful { get; set; }

    }
}
