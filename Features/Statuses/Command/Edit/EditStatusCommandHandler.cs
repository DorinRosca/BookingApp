using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.Entities;
using CarBookingApp.Features.Statuses.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Statuses.Command.Edit
{
    public class EdtStatusCommandHandler : IRequestHandler<EditStatusCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EdtStatusCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditStatusCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The model parameter cannot be null.");
            }

            var entity = _mapper.Map<Status>(request);
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
