using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Brands.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Brands.Query.GetById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetBrandByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BrandViewModel> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.BrandId == 0)
            {
                throw new ArgumentNullException(nameof(request.BrandId), "The Id parameter cannot be zero.");

            }

            var entity = await _context.Brand.FindAsync(request.BrandId);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.BrandId), "There is no entity with such Id.");

            }

            var model = _mapper.Map<BrandViewModel>(entity);
            return model;
        }
    }
}
