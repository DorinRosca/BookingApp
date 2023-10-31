using AutoMapper;
using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Transmissions.Command.Delete
{
    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteTransmissionCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            if (request.TransmissionId == 0)
            {
                throw new ArgumentNullException(nameof(request.TransmissionId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.Transmission.FindAsync(request.TransmissionId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.TransmissionId), "There is no entity with such Id.");

            }

            _context.Transmission.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
