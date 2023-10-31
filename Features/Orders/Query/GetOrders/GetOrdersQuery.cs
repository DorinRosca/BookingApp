using CarBookingApp.Features.Orders.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Orders.Query.GetOrders
{
    public record GetOrdersQuery : IRequest<IEnumerable<OrderViewModel>>;
}
