using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Cars.Query.GetEditCar
{
    public class GetEditCarQueryHandler : IRequestHandler<GetEditCarQuery, CarEditViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetEditCarQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CarEditViewModel> Handle(GetEditCarQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new ArgumentNullException(nameof(request.Id), "The Id cannot be zero");
            }
            var model = await _context.Car.FindAsync(request.Id);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(request.Id), "There is no entity with such Id");
            }

            var viewmodel = _mapper.Map<CarEditViewModel>(model);

            viewmodel.Brand = await _context.Brand.ToListAsync(cancellationToken);
            viewmodel.Drive = await _context.Drive.ToListAsync(cancellationToken);
            viewmodel.FuelType = await _context.FuelType.ToListAsync(cancellationToken);
            viewmodel.Transmission = await _context.Transmission.ToListAsync(cancellationToken);
            viewmodel.Vehicle = await _context.Vehicle.ToListAsync(cancellationToken);
            return viewmodel;
        }
    }
}
