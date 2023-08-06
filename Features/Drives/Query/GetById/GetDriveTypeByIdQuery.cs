using CarBookingApp.Features.Drives.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Drives.Query.GetById
{
    public class GetDriveByIdQuery : IRequest<DriveViewModel>
    {
        public byte DriveId { get; set; }

        public GetDriveByIdQuery(byte Id)
        {
            DriveId = Id;
        }
    }
}
