using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Car_Booking_App.Entities
{
          [Table("Order")]
     public class Order
     {

          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int OrderId { get; set; }

          [Required]
          [ForeignKey(nameof(User))]
          public string UserId { get; set; }

          [Required, ForeignKey("Car")]
          public int CarId { get; set; }

          public Car Car { get; set; }

          [Required, Column(TypeName = "datetime")]
          public DateTime RentalStartDate { get; set; }

          [Required, Column(TypeName = "datetime")]
          public DateTime RentalEndDate { get; set; }

          [Required,Range(0,double.MaxValue), Column(TypeName = "decimal(8,2)")]
          public double TotalAmount { get; set; }

          [Required,ForeignKey("Status")]
          public byte StatusId { get; set; }
          public Status Status { get; set; }
          public IdentityUser User { get; set; }  

     }
}
