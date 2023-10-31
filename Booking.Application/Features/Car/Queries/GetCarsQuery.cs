using AutoMapper;
using Booking.Application.Contracts.Database.Car;
using MediatR;
using Booking.Application.Features.Brand;
using Booking.Application.Features.Brand.Queries;
using Booking.Application.Features.Vehicle;
using Booking.Application.Features.Vehicle.Queries;

namespace Booking.Application.Features.Car.Queries
{
    public record GetCarsQuery(CarFilterModel Model) : IRequest<CarFilterModel>
    {
        // List of Cars
        public List<CarModel>? Cars = Model.Cars;
        // Selected Filter
        public List<byte?>? SelectedVehicleId = Model.SelectedVehicleId;

        public List<byte?>? SelectedBrandId = Model.SelectedBrandId;
        // ALL Filters
        public string? SearchCarName = Model.SearchCarName;
        public IEnumerable<VehicleModel>? Vehicles = Model.Vehicles;
        public IEnumerable<BrandModel>? Brands = Model.Brands;
    }

    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, CarFilterModel>
    {
        private readonly IMapper _mapper;
        private readonly IGetCarsAsQueryable _getAll;
        private readonly IMediator _mediator;

        public GetCarsQueryHandler(IGetCarsAsQueryable selectAll, IMapper mapper, IMediator mediator)
        {
            _getAll = selectAll;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CarFilterModel> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _getAll.GetAsync();

            if (request.SelectedBrandId != null && request.SelectedBrandId.Any())
            {
                cars = cars.Where(c => request.SelectedBrandId.Contains(c.BrandId)).ToList();
            }

            if (request.SelectedVehicleId != null && request.SelectedVehicleId.Any())
            {
                cars = cars.Where(c => request.SelectedVehicleId.Contains(c.VehicleId)).ToList();
            }

            if (request.SearchCarName != null)
            {
                cars = cars.Where(c => c.ModelName != null && c.ModelName.Contains(request.SearchCarName)).ToList();
            }

            var getVehiclesQuery = new GetVehiclesQuery();
            var getBrandsQuery = new GetBrandsQuery();
            var vehicles = await _mediator.Send(getVehiclesQuery, cancellationToken);
            var brands = await _mediator.Send(getBrandsQuery, cancellationToken);
            var carModels = _mapper.Map<List<CarModel>>(cars);

            var result = new CarFilterModel
            {
                Cars = carModels,
                Vehicles = vehicles,
                Brands = brands
            };

            return result;
        }
    }
}
