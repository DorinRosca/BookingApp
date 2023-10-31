namespace Booking.Application.Contracts.Database.Order
{
     public interface IGetAllOrders
     {
          Task<IEnumerable<Domain.Entities.Order>> GetAsync();

     }
}
