using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;

namespace CarBookingApp.Features.FuelTypes.Query.GetAll
{
    public class GetAllFuelTypeQuery : IRequest<IEnumerable<FuelTypeViewModel>>
    {
    }
}
