using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Vehicle.Queries
{
    public record GetVehicleQuery(byte Id) : IRequest<VehicleModel?>
    {
        public byte VehicleId = Id;
    }
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, VehicleModel?>
    {
        private readonly IGetEntityById<Domain.Entities.Vehicle> _getById;
        private readonly IMapper _mapper;

        public GetVehicleQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.Vehicle> getById)
        {
            _mapper = mapper;
            _getById = getById;
        }

        public async Task<VehicleModel?> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) { return null; }
            var vehicle = await _getById.GetByIdAsync(request.Id);

            return vehicle is not null ? _mapper.Map<VehicleModel>(vehicle) : null;
        }
    }
}
