using AutoMapper;
using CarBookingApp.Data;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace CarBookingApp.Features.Roles.Command.Delete
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeleteRoleCommandHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
             if (request.RoleId.IsNullOrEmpty())
             {
                  throw new ArgumentNullException(nameof(request.RoleId), "The Id cannot be null");
             }

             var entity = await _context.Roles.FindAsync(request.RoleId,cancellationToken);
             if (entity == null)
             {
                  throw new ArgumentNullException(nameof(entity), "There is no entity with such Id");
             }
             _context.Roles.Remove(entity);
             var result = await _context.SaveChangesAsync(cancellationToken);

             return result > 0;
          }
    }
}
