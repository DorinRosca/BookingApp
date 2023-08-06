using AutoMapper;
using CarBookingApp.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Roles.Command.Edit
{
    public class EdtRoleCommandHandler : IRequestHandler<EditRoleCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EdtRoleCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {

             if (request == null)
             {
                  throw new ArgumentNullException(nameof(request), "Model cannot be null");
             }

             var entity = _mapper.Map<IdentityRole>(request);
             entity.NormalizedName = entity.Name;
             _context.Entry(entity).State = EntityState.Modified;
             var result = await _context.SaveChangesAsync(cancellationToken);
             return result > 0;
          }
    }
}
