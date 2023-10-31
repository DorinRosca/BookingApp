namespace Booking.Application.Contracts.Database
{
     public interface IUpdateEntity<TEntity> where TEntity : class
     {
          Task<bool> UpdateAsync(TEntity entity);
     }
}
