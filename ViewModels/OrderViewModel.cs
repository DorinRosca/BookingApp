using Car_Booking_App.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CarBookingApp.Attribute;

namespace CarBookingApp.ViewModels
{
     public class OrderViewModel
     {
          public int OrderId { get; set; }
          [Required]
          public int CarId { get; set; }

          public Car Car { get; set; }
          [Required]

          public string UserId { get; set; }
          public IdentityUser User { get; set; }
          [Required]
          [EarlierThanNow(ErrorMessage = "Start date cannot be earlier than the current date.")]
          public DateTime RentalStartDate { get; set; }

          [Required]
          [RentalEndDate(ErrorMessage = "End date cannot be earlier than the start date.")]
          public DateTime RentalEndDate { get; set; }

          public DateTime Now { get; set; }
          public double TotalAmount { get; set; }

          public byte StatusId { get; set; }
          public Status Status { get; set; }

          public OrderViewModel()
          {
               Now = DateTime.Today;
          }
     }
}
