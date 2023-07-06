using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Seats")]
     public class Seats
     {
               [Required]
               [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
               public int SeatsId { get; set; }

               [Required, Range(1,byte.MaxValue), Column(TypeName = "smallint")]
               public byte SeatsNumber { get; set; }
     }
}
