using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Vehicles.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Vehicles.Query.GetAll
{
    public class GetAllVehicleQueryHandler : IRequestHandler<GetAllVehicleQuery, IEnumerable<VehicleViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllVehicleQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }
        public async Task<IEnumerable<VehicleViewModel>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
          {
            var dataList = await _appCache.GetOrAddAsync("AllVehicles.Get", () => _context.Vehicle.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<VehicleViewModel>(entity));
            return result;
        }
    }
}
