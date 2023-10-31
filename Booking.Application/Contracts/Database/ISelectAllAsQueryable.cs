namespace Booking.Application.Contracts.Database
{
     public interface ISelectAllAsQueryable<TEntity> where TEntity : class
     {
          Task<IQueryable<TEntity>> GetAllAsQueryable();

     }
}
