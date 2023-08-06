using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Brands.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Brands.Query.GetAll
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IEnumerable<BrandViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllBrandQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandViewModel>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.Brand.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<BrandViewModel>(entity));
            return result;
        }
    }
}
