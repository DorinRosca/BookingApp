using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;

namespace CarBookingApp.Features.FuelTypes.Query.GetById
{
    public class GetFuelTypeByIdQueryHandler : IRequestHandler<GetFuelTypeByIdQuery, FuelTypeViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetFuelTypeByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FuelTypeViewModel> Handle(GetFuelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.FuelTypeId == 0)
            {
                throw new ArgumentNullException(nameof(request.FuelTypeId), "The Id parameter cannot be zero.");

            }

            var entity = await _context.FuelType.FindAsync(request.FuelTypeId);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.FuelTypeId), "There is no entity with such Id.");

            }

            var model = _mapper.Map<FuelTypeViewModel>(entity);
            return model;
        }
    }
}
