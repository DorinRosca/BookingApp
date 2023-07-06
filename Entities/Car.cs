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


          [Required, ForeignKey("Vehicle")]
          public int VehicleId { get; set; }
          public Vehicle Vehicle { get; set; }


          [Required, ForeignKey("Brand")] 
          public int BrandId { get; set; }
          public Brand Brand { get; set; }

          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string Model { get; set; }

           
          [Required, Range(0, short.MaxValue), Column(TypeName = "smallint")]
          public short Year { get; set; }


          [Required, StringLength(20), Column(TypeName = "nvarchar(20)")]
          public string LicensePlate { get; set; }

          [Required, ForeignKey("FuelType")] 
          public int FuelTypeId { get; set; }
          public FuelType FuelType { get; set; }


          [Required, ForeignKey("Transmission")] 
          public int TransmissionId { get; set; }
          public Transmission Transmission { get; set; }


          [Required, ForeignKey("Drive")] 
          public int DriveId { get; set; }
          public Drive Drive { get; set; }

          [Required, Range(0, Double.MaxValue), Column(TypeName = "decimal(8,2)")]
          public decimal PricePerDay { get; set; }
          
          [Required, Column(TypeName = "bit")] public bool Availability { get; set; }

     }
}
