using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Roles.Query.GetById;
using CarBookingApp.Features.Roles.ViewModel;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace CarBookingApp.Features.Roles.Query.GetById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<RoleViewModel> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
             if (request.RoleId.IsNullOrEmpty())
             {
                  throw new ArgumentNullException(nameof(request.RoleId), "The Id cannot be null");
             }

             var entity = await _context.Roles.FindAsync(request.RoleId);
             if (entity == null)
             {
                  throw new ArgumentNullException(nameof(entity), "There is no Role with such Id");
             }

             var model = _mapper.Map<RoleViewModel>(entity);

             return model;
          }
    }
}
