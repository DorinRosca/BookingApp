using Microsoft.AspNetCore.Http;
using Booking.Application.Features.Brand;
using Booking.Application.Features.Drive;
using Booking.Application.Features.FuelType;
using Booking.Application.Features.Transmission;
using Booking.Application.Features.Vehicle;

namespace Booking.Application.Features.Car
{
     public class CarViewModel
     {
          public int? CarId { get; set; }
          public string? ModelName { get; set; }
          public short? Year { get; set; }
          public decimal? PricePerDay { get; set; }
          public byte[]? Image { get; set; }

          public byte? VehicleId { get; set; }
          public byte? BrandId { get; set; }
          public byte? FuelTypeId { get; set; }
          public byte? TransmissionId { get; set; }
          public byte? DriveId { get; set; }
          public IFormFile? ImageFile { get; set; }

          public IEnumerable<BrandModel>? Brands { get; set; }
          public IEnumerable<DriveModel>? Drives { get; set; }
          public IEnumerable<FuelTypeModel>? FuelTypes { get; set; }
          public IEnumerable<TransmissionModel>? Transmissions { get; set; }
          public IEnumerable<VehicleModel>? Vehicles { get; set; }
     }
}
