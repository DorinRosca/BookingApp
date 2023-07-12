using Car_Booking_App.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CarBookingApp.ViewModels
{
     public class CarViewModel
     {
          public int CarId { get; set; }
          public byte BrandId { get; set; }
          public byte VehicleId { get; set; }
          [StringLength(50)]
          public string ModelName { get; set; }
          [Range(0, short.MaxValue)]
          public short Year { get; set; }
          public byte FuelTypeId { get; set; }
          public byte TransmissionId { get; set; }
          public byte DriveId { get; set; }
          public decimal PricePerDay { get; set; }
          public byte[] Image { get; set; }



          public Vehicle Vehicle { get; set; }
          public Brand Brand { get; set; }
          public FuelType FuelType { get; set; }
          public Transmission Transmission { get; set; }
          public Drive Drive { get; set; }
          [Range(0, Double.MaxValue)]
          public IFormFile ImageFile { get; set; }

     }
}
