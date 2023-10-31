using CarBookingApp.Data;
using CarBookingApp.Features.Statuses.Entities;
using MediatR;
using Newtonsoft.Json.Linq;

namespace CarBookingApp.Features.Statuses.Command.Create
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, bool>
    {
        private readonly DataContext _context;

        public CreateStatusCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var status = new Status
            {
                StatusName = request.StatusName
            };
            await _context.Status.AddAsync(status, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
