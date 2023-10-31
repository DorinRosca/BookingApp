using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Drive.Queries
{
    public record GetDrivesQuery : IRequest<IEnumerable<DriveModel>?>;

    public class GetDrivesQueryHandler : IRequestHandler<GetDrivesQuery, IEnumerable<DriveModel>?>
    {
        private readonly IMapper _mapper;
        private readonly ISelectAll<Domain.Entities.Drive> _selectAll;

        public GetDrivesQueryHandler(ISelectAll<Domain.Entities.Drive> selectAll, IMapper mapper)
        {
            _selectAll = selectAll;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriveModel>?> Handle(GetDrivesQuery request, CancellationToken cancellationToken)
        {
            var drives = await _selectAll.GetDataAsync();
            return drives.Any() ? _mapper.Map<List<DriveModel>>(drives) : null;
        }
    }
}
