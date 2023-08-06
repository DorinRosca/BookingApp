using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.FuelTypes.Query.GetAll
{
    public class GetAllFuelTypeQueryHandler : IRequestHandler<GetAllFuelTypeQuery, IEnumerable<FuelTypeViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllFuelTypeQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FuelTypeViewModel>> Handle(GetAllFuelTypeQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.FuelType.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<FuelTypeViewModel>(entity));
            return result;
        }
    }
}
