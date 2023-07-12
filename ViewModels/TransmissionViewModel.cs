using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class TransmissionViewModel
     {
          public byte TransmissionId { get; set; }

          [Required, StringLength(50)]
          public string TransmissionName { get; set; }
     }
}
