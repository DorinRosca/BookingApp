using CarBookingApp.Features.Orders.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Orders.Query.GetUserOrders
{
    public record GetUserOrderQuery(string Id) :IRequest<IEnumerable<OrderViewModel>>
     {
          public string UserId = Id;
     }
}
