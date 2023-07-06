using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("FuelType")]
     public class FuelType
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int FuelTypeId { get; set; }

          [Required, StringLength(50), Column(TypeName = "varchar(50)")]
          public string FuelTypeName { get; set; }
     }
}
