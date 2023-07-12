using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class VehicleViewModel
     {
          public byte VehicleId { get; set; }

          [Required, StringLength(50)]
          public string VehicleName { get; set; }


          [Required, Range(1, byte.MaxValue)]
          public byte SeatsNumber { get; set; }
     }
}
