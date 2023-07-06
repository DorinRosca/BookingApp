using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Booking_App.Entities
{
          [Table("User")]
     public class User
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int UserId { get; set; }

          [Required, StringLength(50), Column(TypeName = "varchar(50)")]
          public string Username { get; set; }

          [Required, StringLength(20), Column(TypeName = "varchar(20)")]
          public string Email { get; set; }

          [Required, StringLength(50),MinLength(8), Column(TypeName = "varchar(50)")]
          public string Password { get; set; }

          [Required,ForeignKey("Role")]
          public int RoleId { get; set; }
     }
}
