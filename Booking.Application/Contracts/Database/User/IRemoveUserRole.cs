using Booking.Domain.Entities;

namespace Booking.Application.Contracts.Database.User
{
     public interface IRemoveUserRole
     {
          Task<bool> RemoveAsync(ApplicationUser user, string roleName);

     }
}
