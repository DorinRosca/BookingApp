using Car_Booking_App.Entities;

namespace CarBookingApp.ViewModels
{
     public class CarEditViewModel
     {
          public int CarId { get; set; }
          public byte VehicleId { get; set; }
          public byte BrandId { get; set; }
          public string ModelName { get; set; }
          public short Year { get; set; }
          public byte FuelTypeId { get; set; }
          public byte TransmissionId { get; set; }
          public byte DriveId { get; set; }
          public decimal PricePerDay { get; set; }

          public byte[] Image { get; set; }
          public bool Availability { get; set; }
          public List<Brand> Brand { get; set; }
          public List<Drive> Drive { get; set; }
          public List<FuelType> FuelType { get; set; }
          public List<Transmission> Transmission { get; set; }
          public List<Vehicle> Vehicle { get; set; }
          public IFormFile ImageFile { get; set; }
     }
}
