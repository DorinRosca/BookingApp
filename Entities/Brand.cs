using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
     [Table("Brand")]
     public class Brand
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int BrandId { get; set; }

          [Required, StringLength(50),Column(TypeName = "nvarchar(50)")]
          public string BrandName { get; set; }

     }
}
