using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Brands.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Brands.Command.Edit
{
    public class EditBrandCommandHandler : IRequestHandler<EditBrandCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditBrandCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The model parameter cannot be null.");
            }

            var entity = _mapper.Map<Brand>(request);
            _context.Entry(entity).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
