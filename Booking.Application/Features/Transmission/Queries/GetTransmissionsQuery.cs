using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Transmission.Queries
{

    public record GetTransmissionsQuery : IRequest<IEnumerable<TransmissionModel>?>;

    public class GetTransmissionsQueryHandler : IRequestHandler<GetTransmissionsQuery, IEnumerable<TransmissionModel>?>
    {
        private readonly IMapper _mapper;
        private readonly ISelectAll<Domain.Entities.Transmission> _selectAll;

        public GetTransmissionsQueryHandler(ISelectAll<Domain.Entities.Transmission> selectAll, IMapper mapper)
        {
            _selectAll = selectAll;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransmissionModel>?> Handle(GetTransmissionsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<TransmissionModel>();
            var transmissions = await _selectAll.GetDataAsync();
            if (transmissions.Any())
            {
                result = _mapper.Map<List<TransmissionModel>>(transmissions);
            }
            return result;
        }
    }
}
