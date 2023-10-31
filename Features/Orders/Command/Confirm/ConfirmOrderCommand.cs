using MediatR;

namespace CarBookingApp.Features.Orders.Command.Confirm
{
     public record ConfirmOrderCommand(int Id) : IRequest<bool>
     {
          public int Id = Id;
     }
}
