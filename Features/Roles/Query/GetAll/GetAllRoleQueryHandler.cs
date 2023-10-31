using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Roles.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Roles.Query.GetAll
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, IEnumerable<RoleViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllRoleQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }
         
        public async Task<IEnumerable<RoleViewModel>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
          {
             var list = await _appCache.GetOrAddAsync("AllRoles.Get", () => _context.Roles.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
             var result = list.Select(entity => _mapper.Map<RoleViewModel>(entity));
             return result;
          }
    }
}
