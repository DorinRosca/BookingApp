using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Car_Booking_App.Entities
{
     [Table("Drive")]
     public class Drive
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public byte DriveId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string DriveName { get; set; }
     }
}
