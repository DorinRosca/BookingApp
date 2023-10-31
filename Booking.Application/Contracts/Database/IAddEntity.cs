namespace Booking.Application.Contracts.Database
{
     public interface IAddEntity<TEntity> where TEntity : class
     {
          Task<bool> InsertAsync(TEntity entity);
     }
}
