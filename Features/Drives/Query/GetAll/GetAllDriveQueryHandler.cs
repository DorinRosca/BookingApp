using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Drives.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Drives.Query.GetAll
{
    public class GetAllDriveQueryHandler : IRequestHandler<GetAllDriveQuery, IEnumerable<DriveViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllDriveQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriveViewModel>> Handle(GetAllDriveQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.Drive.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<DriveViewModel>(entity));
            return result;
        }
    }
}
