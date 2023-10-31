using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Contracts.Database.User
{
     public interface IRegister
     {
          Task<IdentityResult> ApplyAsync(string email, string password);

     }
}
