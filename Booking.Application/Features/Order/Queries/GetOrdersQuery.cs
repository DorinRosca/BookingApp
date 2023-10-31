using AutoMapper;
using Booking.Application.Contracts.Database.Order;
using MediatR;

namespace Booking.Application.Features.Order.Queries
{

    public record GetOrdersQuery : IRequest<IEnumerable<OrderModel>?>;

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderModel>?>
    {
        private readonly IMapper _mapper;
        private readonly IGetAllOrders _selectAll;

        public GetOrdersQueryHandler(IGetAllOrders selectAll, IMapper mapper)
        {
            _selectAll = selectAll;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderModel>?> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = new List<OrderModel>();
            var orders = await _selectAll.GetAsync();
            if (orders.Any())
            {
                result = _mapper.Map<List<OrderModel>>(orders);
            }
            return result;
        }
    }
}
