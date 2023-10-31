using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Transmissions.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Transmissions.Query.GetAll
{
    public class GetAllTransmissionQueryHandler : IRequestHandler<GetAllTransmissionQuery, IEnumerable<TransmissionViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly  IAppCache _appCache;

        public GetAllTransmissionQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }

        public async Task<IEnumerable<TransmissionViewModel>> Handle(GetAllTransmissionQuery request, CancellationToken cancellationToken)
          {
            var dataList = await _appCache.GetOrAddAsync("AllTransmissions.Get", () => _context.Transmission.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<TransmissionViewModel>(entity));
            return result;
        }
    }
}
