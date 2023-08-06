using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;

namespace CarBookingApp.Features.FuelTypes.Query.GetById
{
    public class GetFuelTypeByIdQuery : IRequest<FuelTypeViewModel>
    {
        public byte FuelTypeId { get; set; }

        public GetFuelTypeByIdQuery(byte Id)
        {
            FuelTypeId = Id;
        }
    }
}
