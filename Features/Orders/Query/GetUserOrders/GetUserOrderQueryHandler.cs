using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Orders.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Query.GetUserOrders
{
    public class GetUserOrderQueryHandler :IRequestHandler<GetUserOrderQuery, IEnumerable<OrderViewModel>>
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public GetUserOrderQueryHandler(DataContext context, IMapper mapper)
          {
               _context = context;
               _mapper = mapper;
          }
          public async Task<IEnumerable<OrderViewModel>> Handle(GetUserOrderQuery request, CancellationToken cancellationToken)
          {
               if (request.UserId == null)
               {
                    throw new ArgumentNullException(nameof(request.UserId), "UserId cannot be null");
               }
               var query = _context.Order
                    .Include(o => o.Car)
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .Where(c => c.UserId.Contains(request.UserId));

               var orders = await query.ToListAsync(cancellationToken);

               var orderViewModels = orders.Select(entity => _mapper.Map<OrderViewModel>(entity));
               return orderViewModels;
          }
     }
}
