using Booking.Application.Features.Brand.Queries;
using Booking.Application.Features.Drive.Queries;
using Booking.Application.Features.FuelType.Queries;
using Booking.Application.Features.Transmission.Queries;
using Booking.Application.Features.Vehicle.Queries;
using MediatR;

namespace Booking.Application.Features.Car.Queries
{
    public record GetCarDetailsQuery : IRequest<CarViewModel?>;

    public class GetDetailsQueryHandler : IRequestHandler<GetCarDetailsQuery, CarViewModel?>
    {

         private readonly IMediator _mediator;

         public GetDetailsQueryHandler(IMediator mediator)
         {
              _mediator = mediator;
         }

         public async Task<CarViewModel?> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = new CarViewModel
            {
                Brands = await _mediator.Send(new GetBrandsQuery(), cancellationToken),
                Drives = await _mediator.Send(new GetDrivesQuery(), cancellationToken),
                FuelTypes = await _mediator.Send(new GetFuelTypesQuery(), cancellationToken),
                Transmissions = await _mediator.Send(new GetTransmissionsQuery(), cancellationToken),
                Vehicles = await _mediator.Send(new GetVehiclesQuery(), cancellationToken),
            };

            return data;
        }
    }
}
