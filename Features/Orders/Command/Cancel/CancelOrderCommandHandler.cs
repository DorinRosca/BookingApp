using CarBookingApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Command.Cancel
{
     public class CancelOrderCommandHandler :IRequestHandler<CancelOrderCommand, bool>
     {
          private readonly DataContext _context;

          public CancelOrderCommandHandler(DataContext context)
          {
               this._context = context;
          }

          public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
          {
               if (request.Id == 0)
               {
                    throw new ArgumentNullException(nameof(request.Id), "The Id cannot be zero");
               }
               var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == request.Id,cancellationToken);
               if (order == null)
               {
                    throw new ArgumentNullException(nameof(request.Id), "There is no order with such Id");
               }
               var cancelledStatus = await _context.Status.FirstOrDefaultAsync(s => s.StatusName == "Cancelled",cancellationToken);
               if (cancelledStatus != null)
               {
                    order.StatusId = cancelledStatus.StatusId; // Assign the ID of the "Confirmed" status
                    var result = await _context.SaveChangesAsync(cancellationToken);
                    return result > 0;
               }
               else
               {
                    throw new ArgumentNullException(nameof(request.Id), "There is no Cancelled status");

               }
          }
     }
}
