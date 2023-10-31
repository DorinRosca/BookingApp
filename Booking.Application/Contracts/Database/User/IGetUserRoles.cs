using Booking.Domain.Entities;

namespace Booking.Application.Contracts.Database.User
{
     public interface IGetUserRoles
     {
          Task<IList<string>?> GetAsync(ApplicationUser user);

     }
}
