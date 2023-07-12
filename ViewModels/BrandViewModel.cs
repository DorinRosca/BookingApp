using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class BrandViewModel
     {
          public byte BrandId { get; set; }

          [Required, StringLength(50)]
          public string BrandName { get; set; }
     }
}
