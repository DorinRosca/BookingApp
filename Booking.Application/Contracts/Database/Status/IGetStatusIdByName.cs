namespace Booking.Application.Contracts.Database.Status
{
     public interface IGetStatusIdByName
     {
          Task<byte?> GetAsync( string name);
     }
}
