using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Booking_App.Entities
{
          [Table("Vehicle")]
     public class Vehicle
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int VehicleId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string VehicleName { get; set;}


          [Required, Range(1, byte.MaxValue), Column(TypeName = "smallint")]
          public byte SeatsNumber { get; set; }
     }
}
