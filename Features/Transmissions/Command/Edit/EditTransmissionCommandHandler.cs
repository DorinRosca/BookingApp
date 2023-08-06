using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Transmissions.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Transmissions.Command.Edit
{
    public class EdtStatusCommandHandler : IRequestHandler<EditTransmissionCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EdtStatusCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditTransmissionCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The model parameter cannot be null.");
            }

            var entity = _mapper.Map<Transmission>(request);
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
