using CarBookingApp.Features.Drives.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Drives.Command.Create
{
    public class CreateDriveCommand : IRequest<bool>
    {
        [Required, StringLength(50)]
        public string DriveName { get; set; }

        public CreateDriveCommand(DriveViewModel model)
        {
            DriveName = model.DriveName;
        }
    }
}
