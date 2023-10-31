using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Query.GetById
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<VehicleViewModel> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.VehicleId == 0)
            {
                throw new ArgumentNullException(nameof(request.VehicleId), "The Id parameter cannot be zero.");

            }

            var entity = await _context.Vehicle.FindAsync(request.VehicleId);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.VehicleId), "There is no entity with such Id.");

            }

            var model = _mapper.Map<VehicleViewModel>(entity);
            return model;
        }
    }
}
