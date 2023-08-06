using AutoMapper;
using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Statuses.Command.Delete
{
    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteStatusCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.StatusId == 0)
            {
                throw new ArgumentNullException(nameof(request.StatusId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.Status.FindAsync(request.StatusId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.StatusId), "There is no entity with such Id.");

            }

            _context.Status.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
