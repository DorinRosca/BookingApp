namespace Booking.Application.Contracts.Database.Car
{
     public interface IGetCarById
     {
          Task<Domain.Entities.Car?> GetAsync(int? id);
     }
}
