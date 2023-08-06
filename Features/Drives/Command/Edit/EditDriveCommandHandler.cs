using AutoMapper;
using CarBookingApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Drives.Command.Edit
{
    public class EditDriveCommandHandler : IRequestHandler<EditDriveCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditDriveCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditDriveCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The model parameter cannot be null.");
            }

            var entity = _mapper.Map<Entities.Drive>(request);
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
