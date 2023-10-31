using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Statuses.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Statuses.Query.GetAll
{
    public record GetAllStatusQuery : IRequest<IEnumerable<StatusViewModel>>;
}
