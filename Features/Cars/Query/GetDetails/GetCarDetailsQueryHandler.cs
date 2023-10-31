using System.Drawing.Text;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Cars.Query.GetDetails
{
    public class GetCarDetailsQueryHandler : IRequestHandler<GetCarDetailsQuery, CarCreateViewModel>
     {
          private readonly DataContext _context;
          private readonly IAppCache _appCache;

          public GetCarDetailsQueryHandler(DataContext context, IAppCache appCache)
          {
               _context = context;
               _appCache = appCache;
          }
          public async Task<CarCreateViewModel> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
          {

               var viewModel = new CarCreateViewModel
               {
                    Brand =  await _appCache.GetOrAddAsync("AllBrands.Get", () => _context.Brand.ToListAsync(cancellationToken), DateTime.Now.AddHours(4)),
                    Drive =  await _appCache.GetOrAddAsync("AllDrives.Get", () => _context.Drive.ToListAsync(cancellationToken), DateTime.Now.AddHours(4)),
                    FuelType =  await _appCache.GetOrAddAsync("AllFuelTypes.Get", () => _context.FuelType.ToListAsync(cancellationToken), DateTime.Now.AddHours(4)),
                    Transmission =  await _appCache.GetOrAddAsync("AllTransmissions.Get", () => _context.Transmission.ToListAsync(cancellationToken), DateTime.Now.AddHours(4)),
                    Vehicle = await _appCache.GetOrAddAsync("AllVehicles.Get", () => _context.Vehicle.ToListAsync(cancellationToken), DateTime.Now.AddHours(4))
               };

               return viewModel;
          }
     }
}
