using AutoMapper;
using Booking.Application.Contracts.Database;
using MediatR;

namespace Booking.Application.Features.Transmission.Queries
{
    public record GetTransmissionQuery(byte Id) : IRequest<TransmissionModel?>
    {
        public byte TransmissionId = Id;
    }
    public class GetTransmissionQueryHandler : IRequestHandler<GetTransmissionQuery, TransmissionModel?>
    {
        private readonly IGetEntityById<Domain.Entities.Transmission> _getById;
        private readonly IMapper _mapper;

        public GetTransmissionQueryHandler(IMapper mapper, IGetEntityById<Domain.Entities.Transmission> getById)
        {
            _mapper = mapper;
            _getById = getById;
        }

        public async Task<TransmissionModel?> Handle(GetTransmissionQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) { return null; }
            var transmission = await _getById.GetByIdAsync(request.Id);

            return transmission is not null ? _mapper.Map<TransmissionModel>(transmission) : null;
        }
    }
}
