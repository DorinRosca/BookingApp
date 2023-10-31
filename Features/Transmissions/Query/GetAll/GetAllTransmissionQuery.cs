using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Transmissions.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Transmissions.Query.GetAll
{
    public record GetAllTransmissionQuery : IRequest<IEnumerable<TransmissionViewModel>>;
}
