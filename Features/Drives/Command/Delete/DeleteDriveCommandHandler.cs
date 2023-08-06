using AutoMapper;
using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Drives.Command.Delete
{
    public class DeleteDriveCommandHandler : IRequestHandler<DeleteDriveCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteDriveCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteDriveCommand request, CancellationToken cancellationToken)
        {
            if (request.DriveId == 0)
            {
                throw new ArgumentNullException(nameof(request.DriveId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.Drive.FindAsync(request.DriveId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.DriveId), "There is no entity with such Id.");

            }

            _context.Drive.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
