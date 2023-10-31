using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.FuelTypes.Command.Create
{
    public class CreateFuelTypeCommand : IRequest<bool>
    {
        [Required, StringLength(50)]
        public string FuelTypeName { get; set; }

        public CreateFuelTypeCommand(FuelTypeViewModel model)
        {
            FuelTypeName = model.FuelTypeName;
        }
    }
}
