using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Drives.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Drives.Query.GetAll
{
    public class GetAllDriveQueryHandler : IRequestHandler<GetAllDriveQuery, IEnumerable<DriveViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public GetAllDriveQueryHandler(DataContext context, IMapper mapper, IAppCache appCache)
        {
            _context = context;
            _mapper = mapper;
            _appCache = appCache;
        }

        public async Task<IEnumerable<DriveViewModel>> Handle(GetAllDriveQuery request, CancellationToken cancellationToken)
        {
             var dataList =
                  await _appCache.GetOrAddAsync("AllDrives.Get", () => _context.Drive.ToListAsync(cancellationToken), DateTime.Now.AddHours(4));
            var result = dataList.Select(entity => _mapper.Map<DriveViewModel>(entity));
            return result;
        }
    }
}
