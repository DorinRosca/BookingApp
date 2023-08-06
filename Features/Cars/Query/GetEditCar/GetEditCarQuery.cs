using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Cars.Query.GetEditCar
{
    public record GetEditCarQuery(int Id) : IRequest<CarEditViewModel>
    {
        public int Id = Id;

    }
}
