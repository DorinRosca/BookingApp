using AutoMapper;
using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.FuelTypes.Command.Delete
{
    public class DeleteFuelTypeCommandHandler : IRequestHandler<DeleteFuelTypeCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteFuelTypeCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteFuelTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.FuelTypeId == 0)
            {
                throw new ArgumentNullException(nameof(request.FuelTypeId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.FuelType.FindAsync(request.FuelTypeId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.FuelTypeId), "There is no entity with such Id.");

            }

            _context.FuelType.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
