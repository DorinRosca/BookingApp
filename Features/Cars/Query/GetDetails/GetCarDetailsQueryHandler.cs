using System.Drawing.Text;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Cars.Query.GetDetails
{
    public class GetCarDetailsQueryHandler : IRequestHandler<GetCarDetailsQuery, CarCreateViewModel>
     {
          private readonly DataContext _context;

          public GetCarDetailsQueryHandler(DataContext context)
          {
               _context = context;
          }
          public async Task<CarCreateViewModel> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
          {

               var viewModel = new CarCreateViewModel
               {
                    Brand = await _context.Brand.ToListAsync(cancellationToken),
                    Drive = await _context.Drive.ToListAsync(cancellationToken),
                    FuelType = await _context.FuelType.ToListAsync(cancellationToken),
                    Transmission = await _context.Transmission.ToListAsync(cancellationToken),
                    Vehicle = await _context.Vehicle.ToListAsync(cancellationToken)
               };

               return viewModel;
          }
     }
}
