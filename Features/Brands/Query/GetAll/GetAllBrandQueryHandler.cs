using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Brands.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Brands.Query.GetAll
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IEnumerable<BrandViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllBrandQueryHandler(DataContext context, IMapper mapper, IAppCache cache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = cache;
        }

        public async Task<IEnumerable<BrandViewModel>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
             var dataList = await _appCache.GetOrAddAsync("AllBrands.Get", () => _context.Brand.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<BrandViewModel>(entity));
            return result;
        }
    }
}
