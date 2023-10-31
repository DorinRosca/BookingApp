using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Vehicles.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Vehicles.Command.Edit
{
    public class EditVehicleCommandHandler : IRequestHandler<EditVehicleCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditVehicleCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EditVehicleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Vehicle>(request);
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
