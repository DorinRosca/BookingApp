using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Statuses.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Statuses.Query.GetById
{
    public class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, StatusViewModel>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetStatusByIdQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StatusViewModel> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.StatusId == 0)
            {
                throw new ArgumentNullException(nameof(request.StatusId), "The Id parameter cannot be zero.");

            }

            var entity = await _context.Status.FindAsync(request.StatusId);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(request.StatusId), "There is no entity with such Id.");

            }

            var model = _mapper.Map<StatusViewModel>(entity);
            return model;
        }
    }
}
