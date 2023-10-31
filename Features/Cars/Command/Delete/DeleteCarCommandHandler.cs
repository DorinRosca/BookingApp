using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Cars.Command.Delete
{
     public class DeleteCarCommandHandler :IRequestHandler<DeleteCarCommand,bool>
     {
          private readonly DataContext _dataContext;

          public DeleteCarCommandHandler(DataContext dataContext)
          {
               _dataContext = dataContext;
          }

          public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
          {
               if (request.CarId == 0)
               {
                    throw new ArgumentNullException(nameof(request.CarId), "The Id cannot be zero");
               }

               var entity = await _dataContext.Car.FindAsync(request.CarId,cancellationToken);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(request.CarId), "There is no entity with such Id");
               }

               _dataContext.Car.Remove(entity);
               var result = await _dataContext.SaveChangesAsync(cancellationToken);

               return result > 0;
          }
     }
}
