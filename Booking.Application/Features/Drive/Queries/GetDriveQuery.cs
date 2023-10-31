using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Drive.Queries
{
     public record GetDriveQuery(byte Id) : IRequest<DriveModel?>;

     public class GetDriveQueryHandler : IRequestHandler<GetDriveQuery, DriveModel?>
     {
          private readonly IGetEntityById<Domain.Entities.Drive> _getById;
          private readonly IMapper _mapper;

          public GetDriveQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.Drive> getById)
          {
               _mapper = mapper;
               _getById = getById;
          }

          public async Task<DriveModel?> Handle(GetDriveQuery request, CancellationToken cancellationToken)
          {
               if (request.Id == 0)
               {
                    return null;
               }

               var drive = await _getById.GetByIdAsync(request.Id);

               return drive != null ? _mapper.Map<DriveModel>(drive) : null;
          }
     }
}