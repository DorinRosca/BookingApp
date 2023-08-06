using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Transmissions.Command.Delete
{
    public record DeleteTransmissionCommand(byte TransmissionId) : IRequest<bool>
    {
        public byte TransmissionId { get; set; } = TransmissionId;
    }
}
