using CarBookingApp.Features.Transmissions.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Transmissions.Query.GetById
{
    public record GetTransmissionByIdQuery : IRequest<TransmissionViewModel>
    {
        public byte TransmissionId { get; set; }

        public GetTransmissionByIdQuery(byte Id)
        {
            TransmissionId = Id;
        }
    }
}
