using AutoMapper;
using Booking.Application.Contracts.Database;
using Booking.Application.Features.Brand.Queries;
using Booking.Application.Features.Drive.Queries;
using Booking.Application.Features.FuelType.Queries;
using Booking.Application.Features.Transmission.Queries;
using Booking.Application.Features.Vehicle.Queries;
using MediatR;

namespace Booking.Application.Features.Car.Queries
{
    public record GetCarUpdateQuery(int? Id) : IRequest<CarViewModel?>
    {
        public int? Id = Id;
    }
    public class GetEditQueryHandler : IRequestHandler<GetCarUpdateQuery, CarViewModel?>
    {
        private readonly IGetEntityById<Domain.Entities.Car> _getCarById;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetEditQueryHandler(IGetEntityById<Domain.Entities.Car> getCarById, IMediator mediator, IMapper mapper)
        {
             _getCarById = getCarById;
             _mediator = mediator;
             _mapper = mapper;
        }


        public async Task<CarViewModel?> Handle(GetCarUpdateQuery request, CancellationToken cancellationToken)
        {
            var car = await _getCarById.GetByIdAsync(request.Id);
            var data = _mapper.Map<CarViewModel>(car);
            data.Brands = await _mediator.Send(new GetBrandsQuery(), cancellationToken);
            data.Drives = await _mediator.Send(new GetDrivesQuery(), cancellationToken);
            data.FuelTypes = await _mediator.Send(new GetFuelTypesQuery(), cancellationToken);
            data.Transmissions = await _mediator.Send(new GetTransmissionsQuery(), cancellationToken);
            data.Vehicles = await _mediator.Send(new GetVehiclesQuery(), cancellationToken);
            return data;
        }
    }
}
