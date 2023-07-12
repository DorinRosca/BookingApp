using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class StatusViewModel
     {
          public byte StatusId { get; set; }

          [Required, StringLength(50)]
          public string StatusName { get; set; }
     }
}
