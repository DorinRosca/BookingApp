using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Transmissions.ViewModel;

namespace CarBookingApp.Features.Transmissions.Command.Create
{
    public record CreateTransmissionCommand : IRequest<bool>
    {

        [Required, StringLength(50)]
        public string TransmissionName { get; set; }

        public CreateTransmissionCommand(TransmissionViewModel model)
        {
            TransmissionName = model.TransmissionName;
        }
    }
}
