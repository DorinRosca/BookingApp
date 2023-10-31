using CarBookingApp.Features.Vehicles.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Vehicles.Query.GetAll
{
    public record GetAllVehicleQuery : IRequest<IEnumerable<VehicleViewModel>>;
}
