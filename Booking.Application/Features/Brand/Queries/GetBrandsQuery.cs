using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Brand.Queries
{
     public record GetBrandsQuery : IRequest<IEnumerable<BrandModel>?>;

     public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<BrandModel>?>
     {
          private readonly IMapper _mapper;
          private readonly ISelectAll<Domain.Entities.Brand> _selectAll;

          public GetBrandsQueryHandler(ISelectAll<Domain.Entities.Brand> selectAll, IMapper mapper)
          {
               _selectAll = selectAll;
               _mapper = mapper;
          }

          public async Task<IEnumerable<BrandModel>?> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
          {
               var brands = await _selectAll.GetDataAsync();
               return brands.Any() ? _mapper.Map<IEnumerable<BrandModel>>(brands) : null;
          }
     }
}