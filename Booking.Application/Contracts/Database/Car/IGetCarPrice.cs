namespace Booking.Application.Contracts.Database.Car
{
     public interface IGetCarPrice
     {
          Task<double?> GetAsync(int? id, int daysDiff);
     }
}
