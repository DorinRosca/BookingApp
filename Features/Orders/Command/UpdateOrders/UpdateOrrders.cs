using CarBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Command.UpdateOrders
{
     public class UpdateOrrders
     {
          private readonly DataContext _context;

          public UpdateOrrders(DataContext context)
          {
               _context = context;
          }

          public async Task<bool> UpdateStatuses()
          {
               DateTime currentDateTime = DateTime.Now;

               // Update orders with status "Confirmed"
               var confirmedOrdersToUpdate = await _context.Order
                    .Where(o => o.Status.StatusName == "Confirmed" && o.RentalStartDate <= currentDateTime)
                    .ToListAsync();

               // Update orders with status "In Execution"
               var inExecutionOrdersToUpdate = await _context.Order
                    .Where(o => o.Status.StatusName == "In Execution" && o.RentalStartDate <= currentDateTime)
                    .ToListAsync();

               if ((confirmedOrdersToUpdate == null || !confirmedOrdersToUpdate.Any()) &&
                   (inExecutionOrdersToUpdate == null || !inExecutionOrdersToUpdate.Any()))
               {
                    return false;
               }

               // Fetch the "In Execution" status from the database
               var inExecutionStatus = await _context.Status
                    .FirstOrDefaultAsync(s => s.StatusName == "In Execution");

               // Fetch the "Finished" status from the database
               var finishedStatus = await _context.Status
                    .FirstOrDefaultAsync(s => s.StatusName == "Finished");

               // Update orders with status "Confirmed" to "In Execution"
               foreach (var order in confirmedOrdersToUpdate)
               {
                    order.Status = inExecutionStatus;
               }

               // Update orders with status "In Execution" to "Finished"
               foreach (var order in inExecutionOrdersToUpdate)
               {
                    order.Status = finishedStatus;
               }

               var result = await _context.SaveChangesAsync();
               return result > 0;
          }
     }
}
