using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Booking_App.Entities
{
          [Table("Vehicle")]
     public class Vehicle
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public byte VehicleId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string VehicleName { get; set;}


          [Required, Range(1, byte.MaxValue), Column(TypeName = "tinyint")]
          public byte SeatsNumber { get; set; }

          public virtual ICollection<Car> Cars { get; set; }

     }
}
