using Booking.Domain.Entities;

namespace Booking.Application.Contracts.Database.User
{
     public interface IGetUsers
     {
          Task<List<ApplicationUser>> GetAsync();

     }
}
