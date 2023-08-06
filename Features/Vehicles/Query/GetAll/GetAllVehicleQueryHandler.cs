using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Vehicles.Query.GetAll
{
    public class GetAllVehicleQueryHandler : IRequestHandler<GetAllVehicleQuery, IEnumerable<VehicleViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllVehicleQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VehicleViewModel>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.Vehicle.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<VehicleViewModel>(entity));
            return result;
        }
    }
}
