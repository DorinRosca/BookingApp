using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Contracts.Database.User
{
     public interface ILogin
     {
          Task<SignInResult> ApplyAsync(string email, string password,bool remember);

     }
}
