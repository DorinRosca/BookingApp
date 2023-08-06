using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Orders.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Query.GetOrders
{
    public class GetOrdersQueryHandler:IRequestHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
    {
         private readonly DataContext _context;
         private readonly IMapper _mapper;

         public GetOrdersQueryHandler(DataContext context, IMapper mapper)
         {
              _context = context;
              _mapper = mapper;
         }

         public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
         {
              var query = await _context.Order
                   .Include(o => o.Car)
                   .Include(o => o.User)
                   .Include(o => o.Status)
                   .ToListAsync(cancellationToken);
              var orders = query.Select(entity => _mapper.Map<OrderViewModel>(entity));
              return orders;
          }
    }
}
