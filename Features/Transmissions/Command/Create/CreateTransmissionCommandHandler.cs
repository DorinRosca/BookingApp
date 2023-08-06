using CarBookingApp.Data;
using CarBookingApp.Features.Transmissions.Entities;
using MediatR;
using Newtonsoft.Json.Linq;

namespace CarBookingApp.Features.Transmissions.Command.Create
{
    public class CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, bool>
    {
        private readonly DataContext _context;

        public CreateTransmissionCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
        {
            var transmission = new Transmission
            {
                TransmissionName = request.TransmissionName
            };
            await _context.Transmission.AddAsync(transmission, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
