using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Features.Role.Queries
{
     public record GetRolesQuery : IRequest<IEnumerable<RoleModel>?>;

     public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleModel>?>
     {
          private readonly IMapper _mapper;
          private readonly ISelectAll<IdentityRole> _selectAll;

          public GetRolesQueryHandler(ISelectAll<IdentityRole> selectAll, IMapper mapper)
          {
               _selectAll = selectAll;
               _mapper = mapper;
          }

          public async Task<IEnumerable<RoleModel>?> Handle(GetRolesQuery request, CancellationToken cancellationToken)
          {
               var roles = await _selectAll.GetDataAsync();
               var result = roles.Any() ? _mapper.Map<List<RoleModel>>(roles) : null;
               return result;
          }
     }
}