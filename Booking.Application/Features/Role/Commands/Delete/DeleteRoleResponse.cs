using Booking.Application.Responses;

namespace Booking.Application.Features.Role.Commands.Delete
{
    public class DeleteRoleResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
