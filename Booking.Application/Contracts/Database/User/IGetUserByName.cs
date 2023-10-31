using Booking.Domain.Entities;

namespace Booking.Application.Contracts.Database.User
{
     public interface IGetUserByName
     {
          Task<ApplicationUser?> GetAsync(string id);

     }
}
