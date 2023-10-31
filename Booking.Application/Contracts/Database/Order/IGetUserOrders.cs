namespace Booking.Application.Contracts.Database.Order
{
     public interface IGetUserOrders
     {
          Task<IEnumerable<Domain.Entities.Order>> GetAsync(string id);

     }
}
