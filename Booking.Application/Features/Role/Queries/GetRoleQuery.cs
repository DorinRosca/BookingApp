using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Features.Role.Queries
{
     public record GetRoleQuery(string Id) : IRequest<RoleModel?>
     {
          public string RoleId = Id;
     }

     public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleModel?>
     {
          private readonly IGetEntityById<IdentityRole> _getById;
          private readonly IMapper _mapper;

          public GetRoleQueryHandler(IMapper mapper, IGetEntityById<IdentityRole> getById)
          {
               _mapper = mapper;
               _getById = getById;
          }

          public async Task<RoleModel?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
          {
               var role = await _getById.GetByIdAsync(request.RoleId);

               return role is not null ? _mapper.Map<RoleModel>(role) : null;
          }
     }
}