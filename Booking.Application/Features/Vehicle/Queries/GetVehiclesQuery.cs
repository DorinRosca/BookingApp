using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Vehicle.Queries
{

    public record GetVehiclesQuery : IRequest<IEnumerable<VehicleModel>?>;

    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleModel>?>
    {
        private readonly IMapper _mapper;
        private readonly ISelectAll<Domain.Entities.Vehicle> _selectAll;

        public GetVehiclesQueryHandler(ISelectAll<Domain.Entities.Vehicle> selectAll, IMapper mapper)
        {
            _selectAll = selectAll;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleModel>?> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var result = new List<VehicleModel>();
            var vehicles = await _selectAll.GetDataAsync();
            if (vehicles.Any())
            {
                result = _mapper.Map<List<VehicleModel>>(vehicles);
            }
            return result;
        }

    }
}
