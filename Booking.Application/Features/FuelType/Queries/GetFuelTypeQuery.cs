using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.FuelType.Queries
{
     public record GetFuelTypeQuery(byte Id) : IRequest<FuelTypeModel?>;

     public class GetFuelTypeQueryHandler : IRequestHandler<GetFuelTypeQuery, FuelTypeModel?>
     {
          private readonly IGetEntityById<Domain.Entities.FuelType> _getById;
          private readonly IMapper _mapper;

          public GetFuelTypeQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.FuelType> getById)
          {
               _mapper = mapper;
               _getById = getById;
          }

          public async Task<FuelTypeModel?> Handle(GetFuelTypeQuery request, CancellationToken cancellationToken)
          {
               if (request.Id == 0)
               {
                    return null;
               }

               var fuelType = await _getById.GetByIdAsync(request.Id);

               return fuelType is not null ? _mapper.Map<FuelTypeModel>(fuelType) : null;
          }
     }
}