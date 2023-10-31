using AutoMapper;
using Booking.Application.Contracts.Database.Order;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Booking.Application.Features.Order.Queries
{

    public record GetUserOrdersQuery : IRequest<IEnumerable<OrderModel>?>;

    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderModel>?>
    {
        private readonly IMapper _mapper;
        private readonly IGetUserOrders _selectAll;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GetUserOrdersQueryHandler(IGetUserOrders selectAll, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _selectAll = selectAll;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<OrderModel>?> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = new List<OrderModel>();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return result;
            var orders = await _selectAll.GetAsync(userId);
            if (orders.Any())
            {
                 result = _mapper.Map<List<OrderModel>>(orders);
            }
            return result;
        }
    }
}
