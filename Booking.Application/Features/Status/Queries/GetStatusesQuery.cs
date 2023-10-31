using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Status.Queries
{
     public record GetStatusesQuery : IRequest<IEnumerable<StatusModel>?>;

     public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, IEnumerable<StatusModel>?>
     {
          private readonly IMapper _mapper;
          private readonly ISelectAll<Domain.Entities.Status> _selectAll;

          public GetStatusesQueryHandler(ISelectAll<Domain.Entities.Status> selectAll, IMapper mapper)
          {
               _selectAll = selectAll;
               _mapper = mapper;
          }

          public async Task<IEnumerable<StatusModel>?> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
          {
               var statusModels = new List<StatusModel>();
               var statuses = await _selectAll.GetDataAsync();

               if (statuses.Any())
               {
                    statusModels = _mapper.Map<List<StatusModel>>(statuses);
               }

               return statusModels;
          }
     }
}