using Booking.Application.Responses;

namespace Booking.Application.Features.FuelType.Commands.Delete
{
    public class DeleteFuelTypeResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
