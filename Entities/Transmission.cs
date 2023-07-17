using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Transmission")]
     public class Transmission
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public byte TransmissionId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")] 
          public string TransmissionName { get; set; }
          public virtual ICollection<Car> Cars { get; set; }
     }
}