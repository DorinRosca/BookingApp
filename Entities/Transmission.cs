using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Transmission")]
     public class Transmission
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int TransmissionId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")] 
          public string TransmissionName { get; set; }
     }
}