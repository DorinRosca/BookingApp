using Booking.Domain.Entities;

namespace Booking.Application.Contracts.Database.User
{
     public interface ISetRole
     {
          Task<bool> SetAsync(ApplicationUser user, string roleName);

     }
}
