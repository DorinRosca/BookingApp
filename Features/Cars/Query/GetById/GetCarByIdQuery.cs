using CarBookingApp.Features.Cars.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Cars.Query.GetById
{
    public record GetCarByIdQuery(int carId) :IRequest<CarViewModel>
     {
          public int CarId = carId;

     }
}
