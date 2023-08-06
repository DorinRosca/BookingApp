using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Transmissions.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Transmissions.Query.GetAll
{
    public class GetAllTransmissionQueryHandler : IRequestHandler<GetAllTransmissionQuery, IEnumerable<TransmissionViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllTransmissionQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransmissionViewModel>> Handle(GetAllTransmissionQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.Transmission.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<TransmissionViewModel>(entity));
            return result;
        }
    }
}
