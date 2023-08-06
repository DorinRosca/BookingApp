using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Roles.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Roles.Query.GetAll
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, IEnumerable<RoleViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleViewModel>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
             var list = await _context.Roles.ToListAsync(cancellationToken);
             var result = list.Select(entity => _mapper.Map<RoleViewModel>(entity));
             return result;
          }
    }
}
