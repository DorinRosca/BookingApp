using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Statuses.Command.Delete
{
    public record DeleteStatusCommand(byte StatusId) : IRequest<bool>
    {
        public byte StatusId { get; set; } = StatusId;
    }
}
