using CarBookingApp.Features.FuelTypes.ViewModel;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.FuelTypes.Command.Edit
{
    public class EditFuelTypeCommand : IRequest<bool>
    {
        public int FuelTypeId { get; set; }
        [Required, StringLength(50)]
        public string FuelTypeName { get; set; }

        public EditFuelTypeCommand(FuelTypeViewModel model)
        {
            FuelTypeId = model.FuelTypeId;
            FuelTypeName = model.FuelTypeName;
        }
    }
}
