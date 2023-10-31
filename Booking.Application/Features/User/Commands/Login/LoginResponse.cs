using Booking.Application.Responses;

namespace Booking.Application.Features.User.Commands.Login
{
    public class LoginResponse : BaseResponse
    {
        public bool LoginIsSuccessful { get; set; }

    }
}
