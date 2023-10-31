using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.FuelType.Queries
{
     public record GetFuelTypesQuery : IRequest<IEnumerable<FuelTypeModel>?>;

     public class GetFuelTypesQueryHandler : IRequestHandler<GetFuelTypesQuery, IEnumerable<FuelTypeModel>?>
     {
          private readonly IMapper _mapper;
          private readonly ISelectAll<Domain.Entities.FuelType> _selectAll;

          public GetFuelTypesQueryHandler(ISelectAll<Domain.Entities.FuelType> selectAll, IMapper mapper)
          {
               _selectAll = selectAll;
               _mapper = mapper;
          }

          public async Task<IEnumerable<FuelTypeModel>?> Handle(GetFuelTypesQuery request, CancellationToken cancellationToken)
          {
               var result = new List<FuelTypeModel>();
               var fuelTypes = await _selectAll.GetDataAsync();

               if (fuelTypes.Any())
               {
                    result = _mapper.Map<List<FuelTypeModel>>(fuelTypes);
               }

               return result;
          }
     }
}