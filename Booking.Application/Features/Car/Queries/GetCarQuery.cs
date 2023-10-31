using AutoMapper;
using Booking.Application.Contracts.Database.Car;
using MediatR;

namespace Booking.Application.Features.Car.Queries
{
    public record GetCarQuery(int? Id) : IRequest<CarModel?>
    {
        public int? CarId = Id;
    }
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarModel?>
    {
        private readonly IGetCarById _getById;
        private readonly IMapper _mapper;

        public GetCarQueryHandler(IMapper mapper, IGetCarById getById)
        {
            _mapper = mapper;
            _getById = getById;
        }

        public async Task<CarModel?> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) { return null; }
            var car = await _getById.GetAsync(request.CarId);

            return car is not null ? _mapper.Map<CarModel>(car) : null;
        }
    }
}
