using Booking.Application.Responses;

namespace Booking.Application.Features.User.Commands.Register
{
    public class RegisterResponse : BaseResponse
    {
        public bool RegisterIsSuccessful { get; set; }
        public bool LoginIsSuccessful { get; set; }

    }
}
