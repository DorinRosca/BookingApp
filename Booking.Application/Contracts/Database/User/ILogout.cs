namespace Booking.Application.Contracts.Database.User
{
     public interface ILogout
     {
          Task<bool?> ApplyAsync();

     }
}
