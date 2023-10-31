using Booking.Application.Features.Brand;
using Booking.Application.Features.Vehicle;

namespace Booking.Application.Features.Car
{
     public class CarFilterModel
     {
          public List<CarModel>? Cars { get; set; }
          //selected filter
          public List<byte?> SelectedVehicleId { get; set; }
          public List<byte?> SelectedBrandId { get; set; }
          //filters
          public string? SearchCarName { get; set; }
          public IEnumerable<VehicleModel>? Vehicles { get; set; }
          public IEnumerable<BrandModel>? Brands { get; set; }

          public CarFilterModel()
          {
               SelectedBrandId = new List<byte?>();
               SelectedVehicleId = new List<byte?>();
          }
     }
}
