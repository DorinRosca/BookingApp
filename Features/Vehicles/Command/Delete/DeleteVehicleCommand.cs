using MediatR;

namespace CarBookingApp.Features.Vehicles.Command.Delete
{
    public class DeleteVehicleCommand : IRequest<bool>
    {
        public byte VehicleId { get; set; }

        public DeleteVehicleCommand(byte vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}
