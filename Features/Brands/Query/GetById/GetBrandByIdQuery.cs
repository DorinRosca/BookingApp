using CarBookingApp.Features.Brands.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Brands.Query.GetById
{
    public record GetBrandByIdQuery(byte Id) : IRequest<BrandViewModel>
    {
         public byte BrandId = Id;
    }
}
