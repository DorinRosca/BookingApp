namespace Booking.Application.Contracts.Database
{
     public interface ISelectAll<TEntity> where TEntity : class
     {
          Task<IEnumerable<TEntity>> GetDataAsync();
     }
}
