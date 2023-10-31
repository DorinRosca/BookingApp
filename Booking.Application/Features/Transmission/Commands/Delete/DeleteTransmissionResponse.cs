using Booking.Application.Responses;

namespace Booking.Application.Features.Transmission.Commands.Delete
{
    public class DeleteUserRoleResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
