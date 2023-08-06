using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Statuses.ViewModel;

namespace CarBookingApp.Features.Statuses.Command.Edit
{
    public class EditStatusCommand : IRequest<bool>
    {
        public int StatusId { get; set; }
        [Required, StringLength(50)]
        public string StatusName { get; set; }

        public EditStatusCommand(StatusViewModel model)
        {
            StatusId = model.StatusId;
            StatusName = model.StatusName;
        }
    }
}
