using MediatR;

namespace CarBookingApp.Features.Orders.Command.Cancel
{
     public record CancelOrderCommand(int Id) :IRequest<bool>
     {
          public int Id = Id;
     }
}
