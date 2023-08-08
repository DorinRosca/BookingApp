using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Statuses.ViewModel;
using CarBookingApp.Features.Vehicles.Entities;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Statuses.Query.GetAll
{
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, IEnumerable<StatusViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllStatusQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }

        public async Task<IEnumerable<StatusViewModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
             var dataList = await _appCache.GetOrAddAsync("AllStatuses.Get",
                  () => _context.Status.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<StatusViewModel>(entity));
            return result;
        }
    }
}
