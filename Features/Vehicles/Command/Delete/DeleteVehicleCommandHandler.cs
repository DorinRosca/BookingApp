using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Command.Delete
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, bool>
    {

        private readonly DataContext _context;

        public DeleteVehicleCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request.VehicleId == 0)
            {
                throw new ArgumentNullException(nameof(request.VehicleId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.Vehicle.FindAsync(request.VehicleId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.VehicleId), "There is no entity with such Id.");
            }
            _context.Vehicle.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
