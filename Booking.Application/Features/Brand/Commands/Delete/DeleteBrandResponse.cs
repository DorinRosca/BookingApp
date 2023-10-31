using Booking.Application.Responses;

namespace Booking.Application.Features.Brand.Commands.Delete
{
    public class DeleteBrandResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
