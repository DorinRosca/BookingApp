using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Cars.Query.GetEditCar
{
    public class GetEditCarQueryHandler : IRequestHandler<GetEditCarQuery, CarEditViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetEditCarQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }
        public async Task<CarEditViewModel> Handle(GetEditCarQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new ArgumentNullException(nameof(request.Id), "The Id cannot be zero");
            }
            var model = await _context.Car.FindAsync(request.Id);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(request.Id), "There is no entity with such Id");
            }

            var viewmodel = _mapper.Map<CarEditViewModel>(model);
            viewmodel.Brand = await _appCache.GetOrAddAsync("AllBrands.Get",
                 () => _context.Brand.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            viewmodel.Drive = await _appCache.GetOrAddAsync("AllDrives.Get",
                 () => _context.Drive.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            viewmodel.FuelType = await _appCache.GetOrAddAsync("AllFuelTypes.Get",
                 () => _context.FuelType.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            viewmodel.Transmission = await _appCache.GetOrAddAsync("AllTransmissions.Get",
                 () => _context.Transmission.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            viewmodel.Vehicle = await _appCache.GetOrAddAsync("AllVehicles.Get",
                 () => _context.Vehicle.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            return viewmodel;
        }
    }
}
