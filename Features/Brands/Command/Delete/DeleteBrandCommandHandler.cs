using AutoMapper;
using CarBookingApp.Data;
using MediatR;

namespace CarBookingApp.Features.Brands.Command.Delete
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (request.BrandId == 0)
            {
                throw new ArgumentNullException(nameof(request.BrandId), "The Id parameter cannot be zero.");
            }

            var entity = await _context.Brand.FindAsync(request.BrandId, cancellationToken);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.BrandId), "There is no entity with such Id.");

            }

            _context.Brand.Remove(entity);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
