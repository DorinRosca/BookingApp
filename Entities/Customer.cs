using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Customer")]
     public class Customer
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int CustomerId { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string FirstName { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string LastName { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string Email { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string PhoneNumber { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string Address { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string City { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string Country { get; set; }


          [Required, StringLength(20), Column(TypeName = "nvarchar(20)")]
          public string IDNP { get; set; }


     }
}
