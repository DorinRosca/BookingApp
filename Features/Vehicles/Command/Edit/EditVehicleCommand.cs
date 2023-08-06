using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Command.Edit
{
    public class EditVehicleCommand : IRequest<bool>
    {
        public byte VehicleId { get; set; }
        public string VehicleName { get; set; }
        public byte SeatsNumber { get; set; }


        public EditVehicleCommand(VehicleViewModel model)
        {
            VehicleId = model.VehicleId;
            VehicleName = model.VehicleName;
            SeatsNumber = model.SeatsNumber;
        }
    }
}
