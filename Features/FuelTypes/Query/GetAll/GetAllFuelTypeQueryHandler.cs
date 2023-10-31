using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.FuelTypes.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.FuelTypes.Query.GetAll
{
    public class GetAllFuelTypeQueryHandler : IRequestHandler<GetAllFuelTypeQuery, IEnumerable<FuelTypeViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllFuelTypeQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }

        public async Task<IEnumerable<FuelTypeViewModel>> Handle(GetAllFuelTypeQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _appCache.GetOrAddAsync("AllFuelTypes.Get", () => _context.FuelType.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<FuelTypeViewModel>(entity));
            return result;
        }
    }
}
