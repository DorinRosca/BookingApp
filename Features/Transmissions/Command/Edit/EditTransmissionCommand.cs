using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Transmissions.ViewModel;

namespace CarBookingApp.Features.Transmissions.Command.Edit
{
    public class EditTransmissionCommand : IRequest<bool>
    {
        public int TransmissionId { get; set; }

        [Required, StringLength(50)]
        public string TransmissionName { get; set; }
        public EditTransmissionCommand(TransmissionViewModel model)
        {
            TransmissionId = model.TransmissionId;
            TransmissionName = model.TransmissionName;
        }
    }
}
