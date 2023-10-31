namespace Booking.Application.Contracts.Database
{
     public interface IDeleteEntity<TEntity> where TEntity : class
     {
          Task<bool> DeleteAsync(TEntity entity);
     }
}
