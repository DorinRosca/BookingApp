using CarBookingApp.Features.Drives.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Drives.Command.Edit
{
    public class EditDriveCommand : IRequest<bool>
    {
        public int DriveId { get; set; }
        [Required, StringLength(50)]
        public string DriveName { get; set; }

        public EditDriveCommand(DriveViewModel model)
        {
            DriveId = model.DriveId;
            DriveName = model.DriveName;
        }
    }
}
