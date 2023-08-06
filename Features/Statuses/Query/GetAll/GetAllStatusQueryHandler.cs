using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Statuses.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Statuses.Query.GetAll
{
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, IEnumerable<StatusViewModel>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllStatusQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusViewModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var dataList = await _context.Status.ToListAsync(cancellationToken);
            var result = dataList.Select(entity => _mapper.Map<StatusViewModel>(entity));
            return result;
        }
    }
}
