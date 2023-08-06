using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.FuelTypes.ViewModel
{
    public class FuelTypeViewModel
    {
        public byte FuelTypeId { get; set; }

        [Required, StringLength(50)]
        public string FuelTypeName { get; set; }
    }
}
