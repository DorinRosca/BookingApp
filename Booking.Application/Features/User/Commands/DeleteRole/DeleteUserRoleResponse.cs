using Booking.Application.Responses;

namespace Booking.Application.Features.User.Commands.DeleteRole
{
    public class DeleteUserRoleResponse : BaseResponse
    {
        public bool DeleteIsSuccessful { get; set; }

    }
}
