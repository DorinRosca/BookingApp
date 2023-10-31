using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Brand.Queries
{
     public record GetBrandQuery(byte Id) : IRequest<BrandModel?>;

     public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, BrandModel?>
     {
          private readonly IGetEntityById<Domain.Entities.Brand> _getById;
          private readonly IMapper _mapper;

          public GetBrandQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.Brand> getById)
          {
               _mapper = mapper;
               _getById = getById;
          }

          public async Task<BrandModel?> Handle(GetBrandQuery request, CancellationToken cancellationToken)
          {
               if (request.Id == 0)
               {
                    return null;
               }

               var brand = await _getById.GetByIdAsync(request.Id);

               return brand != null ? _mapper.Map<BrandModel>(brand) : null;
          }
     }
}