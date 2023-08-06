using AutoMapper;
using CarBookingApp.Data;
using MediatR;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CarBookingApp.Features.Roles.Command.Create
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(DataContext context, IMapper mapper)
        {
             _context = context;
             _mapper = mapper;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
             if (request == null)
             {
                  throw new ArgumentNullException(nameof(request), "Model cannot be null");
             }

             request.NormalizedName = request.Name;
             var entity = _mapper.Map<CreateRoleCommand, IdentityRole>(request);
             entity.Id = Guid.NewGuid().ToString(); // Generate a new unique identifier for the Id
             await _context.Roles.AddAsync(entity,cancellationToken);
             var result = await _context.SaveChangesAsync(cancellationToken);
             return result > 0;
          }
    }
}
