using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Cars.Query.GetById
{
    public class GetCarByIdQueryHandler :IRequestHandler<GetCarByIdQuery, CarViewModel>
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public GetCarByIdQueryHandler(DataContext context, IMapper mapper)
          {
               _context = context;
               _mapper = mapper;
          }

          public async Task<CarViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
          {
               if (request.CarId == 0)
               {
                    throw new ArgumentNullException(nameof(request.CarId), "The Id cannot be zero");
               }
               var result = await _context.Car
                    .Include(c => c.Brand)
                    .Include(c => c.FuelType)
                    .Include(c => c.Transmission)
                    .Include(c => c.Drive)
                    .Include(c => c.Vehicle)
                    .FirstOrDefaultAsync(c => c.CarId == request.CarId, cancellationToken);
               var car = _mapper.Map<CarViewModel>(result);
               return car;
          }
     }
}
