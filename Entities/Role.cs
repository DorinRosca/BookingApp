using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Role")]
     public class Role
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int RoleId { get; set; }

          [Required, StringLength(50), Column(TypeName = "varchar(50)")]
          public string RoleName { get; set; }
     }
}
