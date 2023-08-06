using MediatR;

namespace CarBookingApp.Features.Drives.Command.Delete
{
    public class DeleteDriveCommand : IRequest<bool>
    {

        public byte DriveId { get; set; }
        public DeleteDriveCommand(byte driveId)
        {
            DriveId = driveId;
        }


    }
}
