using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Booking_App.Entities
{
          [Table("Car")]
     public class Car
     {
          [Required]
          [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int CarId { get; set; }


          [Required,ForeignKey("Vehicle")]
          public int VehicleId { get; set; }


          [Required, ForeignKey("Brand")]
          public int BrandId { get; set; }

          [Required,StringLength(50), Column(TypeName = "varchar(50)")]
          public string Model { get; set; }


          [Required,Range(0,short.MaxValue), Column(TypeName = "tinyint")]
          public short Year { get; set; }

          
          [Required,StringLength(20), Column(TypeName = "varchar(20)")]
          public string LicensePlate { get; set; }

          [Required,Range(0,Double.MaxValue), Column(TypeName = "decimal(8,2)")]
          public double Mileage { get; set; }


          [Required, ForeignKey("FuelType")]
          public int FuelTypeId { get; set; }


          [Required, ForeignKey("Transmission")]
          public int TransmissionId { get; set; }


          
          [Required, ForeignKey("Seats")]
          public int SeatsId { get; set; }



          [Required, ForeignKey("Drive")]
          public int DriveId { get; set; }

          [Required, Range(0,Double.MaxValue), Column(TypeName = "decimal(8,2)")]
          public double PricePerDay { get; set; }

          [Required, Column(TypeName = "bool")]
          public bool Availability { get; set; }

          [Required, Column(TypeName ="BINARY(MAX)")]
          public byte[] Image { get; set; }
     }
}
