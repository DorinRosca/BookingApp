namespace Booking.Application.Contracts.Database.Car
{
     public interface IGetCarsAsQueryable
     {
          Task<List<Domain.Entities.Car>> GetAsync();

     }
}
