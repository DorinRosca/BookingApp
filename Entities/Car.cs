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


          [Required, ForeignKey(nameof(Vehicle))]
          public byte VehicleId { get; set; }
          public Vehicle Vehicle { get; set; }


          [Required, ForeignKey(nameof(Brand))] 
          public byte BrandId { get; set; }
          public Brand Brand { get; set; }


          [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
          public string ModelName { get; set; }

           
          [Required, Range(0, short.MaxValue), Column(TypeName = "smallint")]
          public short Year { get; set; }


          [Required, ForeignKey(nameof(FuelType))] 
          public byte FuelTypeId { get; set; }
          public FuelType FuelType { get; set; }


          [Required, ForeignKey(nameof(Transmission))] 
          public byte TransmissionId { get; set; }
          public Transmission Transmission { get; set; }


          [Required, ForeignKey(nameof(Drive))] 
          public byte DriveId { get; set; }
          public Drive Drive { get; set; }


          [Required, Range(0, Double.MaxValue), Column(TypeName = "decimal(8,2)")]
          public decimal PricePerDay { get; set; }

          [Column(TypeName = "varbinary(max)")]
          public byte[] Image { get; set; }
     }
}
