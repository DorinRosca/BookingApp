using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarBookingApp.Features.Cars.Query.GetAll
{
    public class GetFilteredCarsQueryHandler :IRequestHandler<GetFilteredCarsQuery,CarFilterViewModel>
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public GetFilteredCarsQueryHandler(DataContext context, IMapper mapper)
          {
               _context = context;
               _mapper = mapper;
          }
          public async Task<CarFilterViewModel> Handle(GetFilteredCarsQuery request, CancellationToken cancellationToken)
          {
               using (var context = _context)
               {
                    var query = context.Car
                         .Include(c => c.Brand)
                         .Include(c => c.FuelType)
                         .Include(c => c.Transmission)
                         .Include(c => c.Drive)
                         .Include(c => c.Vehicle)
                         .AsQueryable();
                    if (request != null)
                    {
                         if (!request.SelectedBrandId.IsNullOrEmpty())
                         {
                              query = query.Where(c => request.SelectedBrandId.Contains(c.BrandId));
                         }

                         if (!request.SelectedVehicleId.IsNullOrEmpty())
                         {
                              query = query.Where(c => request.SelectedVehicleId.Contains(c.VehicleId));
                         }

                         if (!string.IsNullOrEmpty(request.SearchCarName))
                         {
                              query = query.Where(c => c.ModelName.Contains(request.SearchCarName));
                         }
                    }

                    var carList = await query.ToListAsync();
                    CarFilterViewModel cars = new CarFilterViewModel();
                    cars.Cars= _mapper.Map<List<CarViewModel>>(carList);

                    cars.Brands = await context.Brand.ToListAsync(cancellationToken);
                    cars.Vehicles = await context.Vehicle.ToListAsync(cancellationToken);

                    return cars;
               }
          }
     }
}
