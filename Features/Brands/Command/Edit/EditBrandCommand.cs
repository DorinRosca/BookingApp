using CarBookingApp.Features.Brands.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Brands.Command.Edit
{
    public record  EditBrandCommand(BrandViewModel model) : IRequest<bool>
    {
        public int BrandId = model.BrandId;
        [Required, StringLength(50)]
        public string BrandName = model.BrandName;

    }
}
