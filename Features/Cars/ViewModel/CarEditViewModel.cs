     using System.ComponentModel.DataAnnotations;
     using CarBookingApp.Features.Brands.Entities;
     using CarBookingApp.Features.Drives.Entities;
     using CarBookingApp.Features.FuelTypes.Entities;
     using CarBookingApp.Features.Transmissions.Entities;
     using CarBookingApp.Features.Vehicles.Entities;

     namespace CarBookingApp.Features.Cars.ViewModel
     {
         public class CarEditViewModel
         {
             public int CarId { get; set; }
             [Required]
             public byte VehicleId { get; set; }
             [Required]
             public byte BrandId { get; set; }

             [Required]
             [StringLength(50)]
             public string ModelName { get; set; }

             [Range(1, short.MaxValue)]
             public short Year { get; set; }

             [Required]
             public byte FuelTypeId { get; set; }
             [Required]
             public byte TransmissionId { get; set; }
             [Required]
             public byte DriveId { get; set; }
             [Required]
             [Range(1, double.PositiveInfinity)]
             public decimal PricePerDay { get; set; }

             public byte[] Image { get; set; }
             public List<Brand> Brand { get; set; }
             public List<Drive> Drive { get; set; }
             public List<FuelType> FuelType { get; set; }
             public List<Transmission> Transmission { get; set; }
             public List<Vehicle> Vehicle { get; set; }
             [Required]
             public IFormFile ImageFile { get; set; }
         }
     }
