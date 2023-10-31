using CarBookingApp.Features.Statuses.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Statuses.Query.GetById
{
    public record GetStatusByIdQuery : IRequest<StatusViewModel>
    {
        public byte StatusId { get; set; }

        public GetStatusByIdQuery(byte Id)
        {
            StatusId = Id;
        }
    }
}
