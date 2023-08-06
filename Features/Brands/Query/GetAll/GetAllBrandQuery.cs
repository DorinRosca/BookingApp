using CarBookingApp.Features.Brands.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Brands.Query.GetAll
{
    public class GetAllBrandQuery : IRequest<IEnumerable<BrandViewModel>>
    {
    }
}
