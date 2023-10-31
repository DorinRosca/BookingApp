using CarBookingApp.Data;
using CarBookingApp.Features.FuelTypes.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.FuelTypes.Command.Create
{
    public class CreateFuelTypeCommandHandler : IRequestHandler<CreateFuelTypeCommand, bool>
    {
        private readonly DataContext _context;
        public CreateFuelTypeCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(CreateFuelTypeCommand request, CancellationToken cancellationToken)
        {
            var fuelType = new FuelType
            {
                FuelTypeName = request.FuelTypeName
            };
            await _context.FuelType.AddAsync(fuelType, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
