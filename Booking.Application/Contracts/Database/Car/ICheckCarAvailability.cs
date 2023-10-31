namespace Booking.Application.Contracts.Database.Car
{
     public interface ICheckCarAvailability
     {
          Task<bool> CheckAsync(int? id,DateTime? starTime,DateTime? endTime);
     }
}
