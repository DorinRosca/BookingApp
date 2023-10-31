namespace Booking.Application.Contracts.Database
{
     public interface IGetEntityById<TEntity> where TEntity : class
     {
          Task<TEntity?> GetByIdAsync<T>(T id);
     }
}
