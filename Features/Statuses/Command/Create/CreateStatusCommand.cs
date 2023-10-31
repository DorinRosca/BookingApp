using MediatR;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Statuses.ViewModel;

namespace CarBookingApp.Features.Statuses.Command.Create
{
    public record CreateStatusCommand(StatusViewModel Model) : IRequest<bool>
    {

        [Required, StringLength(50)]
        public string StatusName = Model.StatusName;

    }
}
