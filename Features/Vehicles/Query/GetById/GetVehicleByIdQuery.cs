using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Query.GetById
{
    public class GetVehicleByIdQuery : IRequest<VehicleViewModel>
    {
        public byte VehicleId { get; set; }

        public GetVehicleByIdQuery(byte id)
        {
            VehicleId = id;
        }
    }
}
