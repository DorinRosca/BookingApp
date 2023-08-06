using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Command.Create
{
    public class CreateVehicleCommand : IRequest<bool>
    {
        public byte VehicleId { get; set; }
        public string VehicleName { get; set; }
        public byte SeatsNumber { get; set; }


        public CreateVehicleCommand(VehicleViewModel model)
        {
            VehicleId = model.VehicleId;
            VehicleName = model.VehicleName;
            SeatsNumber = model.SeatsNumber;
        }
    }
}
