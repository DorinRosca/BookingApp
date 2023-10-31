using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Status.Queries
{
     public record GetStatusQuery(byte Id) : IRequest<StatusModel?>;

     public class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, StatusModel?>
     {
          private readonly IGetEntityById<Domain.Entities.Status> _getById;
          private readonly IMapper _mapper;

          public GetStatusQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.Status> getById)
          {
               _mapper = mapper;
               _getById = getById;
          }

          public async Task<StatusModel?> Handle(GetStatusQuery request, CancellationToken cancellationToken)
          {
               if (request.Id == 0) { return null; }
               var status = await _getById.GetByIdAsync(request.Id);

               return status is not null ? _mapper.Map<StatusModel>(status) : null;
          }
     }
}