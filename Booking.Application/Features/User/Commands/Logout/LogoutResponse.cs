using Booking.Application.Responses;

namespace Booking.Application.Features.User.Commands.Logout
{
     public class LogoutResponse : BaseResponse
     {
          public bool LogoutIsSuccessful { get; set; }
     }
}
