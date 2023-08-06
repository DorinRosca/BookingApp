using CarBookingApp.Features.Drives.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Drives.Query.GetAll
{
    public class GetAllDriveQuery : IRequest<IEnumerable<DriveViewModel>>
    {
    }
}
