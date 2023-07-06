using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Status")]
     public class Status
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int StatusId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string StatusName { get; set; }
     }
}
