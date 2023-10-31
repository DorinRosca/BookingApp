using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Drives.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Drives.Query.GetById
{
    public class GetDriveByIdQueryHandler : IRequestHandler<GetDriveByIdQuery, DriveViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetDriveByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DriveViewModel> Handle(GetDriveByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.DriveId == 0)
            {
                throw new ArgumentNullException(nameof(request.DriveId), "The Id parameter cannot be zero.");

            }

            var entity = await _context.Drive.FindAsync(request.DriveId);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.DriveId), "There is no entity with such Id.");

            }

            var model = _mapper.Map<DriveViewModel>(entity);
            return model;
        }
    }
}
