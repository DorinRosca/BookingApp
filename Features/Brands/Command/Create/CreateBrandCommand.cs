using CarBookingApp.Features.Brands.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Brands.Command.Create
{
    public record CreateBrandCommand(BrandViewModel model) : IRequest<bool>
    {
         [Required, StringLength(50)] 
          public string BrandName = model.BrandName;

    }
}
