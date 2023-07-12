using Car_Booking_App.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class OrderViewModel
     {
          public int OrderId { get; set; }

          public int CarId { get; set; }

          public Car Car { get; set; }
          public string UserId { get; set; }


          public DateTime RentalStartDate { get; set; }

          public DateTime RentalEndDate { get; set; }

          public double TotalAmount { get; set; }

          public byte StatusId { get; set; }
          public Status Status { get; set; }
     }
}
