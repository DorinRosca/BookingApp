using CarBookingApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Command.Confirm
{
     public class ConfirmOrderCommandHandler :IRequestHandler<ConfirmOrderCommand, bool>
     {
          private readonly DataContext _context;

          public ConfirmOrderCommandHandler(DataContext context)
          {
               this._context = context;
          }

          public async Task<bool> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
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

               var confirmedStatus = await _context.Status.FirstOrDefaultAsync(s => s.StatusName == "Confirmed",cancellationToken);
               if (confirmedStatus != null)
               {
                    order.StatusId = confirmedStatus.StatusId; // Assign the ID of the "Confirmed" status
                    var result = await _context.SaveChangesAsync(cancellationToken);
                    return result > 0;
               }
               else
               {
                    throw new ArgumentNullException(nameof(request.Id), "There is no confirmed status");

               }
          }
     }
}
