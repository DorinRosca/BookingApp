using Booking.Application.Responses;

namespace Booking.Application.Features.Vehicle.Commands.Delete
{
     public class DeleteVehicleResponse :BaseResponse
     {
          public bool DeleteIsSuccessful { get; set; }

     }
}
