using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.Entities;
using MediatR;

namespace CarBookingApp.Features.Cars.Command.Create
{
    public class CreateCarCommandHandler :IRequestHandler<CreateCarCommand, bool>
    {
         private readonly  DataContext _dataContext;
         private readonly  IMapper _mapper;

         public CreateCarCommandHandler(DataContext dataContext, IMapper mapper)
         {
              _dataContext = dataContext;
              _mapper = mapper;
         }

         public async Task<bool> Handle(CreateCarCommand request, CancellationToken cancellationToken)
         {
              if (request == null)
              {
                   throw new ArgumentNullException(nameof(request), "The model parameter cannot be null");
              }
              if (request.ImageFile != null && request.ImageFile.Length > 0)
              {
                   using(var memoryStream = new MemoryStream())
                   {
                        await request.ImageFile.CopyToAsync(memoryStream, cancellationToken);
                        request.Image = memoryStream.ToArray();
                   }
              }
              else
              {
                   throw new Exception("Cannot Convert Image");

              }
              var entity = _mapper.Map<Car>(request);

              _dataContext.Car.Add(entity);
              var result = await _dataContext.SaveChangesAsync(cancellationToken);
              return result > 0;
          }
    }
}
