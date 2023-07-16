using Car_Booking_App.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class CarFilterViewModel
     {
          public List<CarViewModel> Cars { get; set; }
          //selected filter
          public List<byte> SelectedVehicleId { get; set; }
          public List<byte> SelectedBrandId { get; set; }
          //filters
          public string SearchCarName { get; set; }
          public List<Vehicle> Vehicles { get; set; }
          public List<Brand> Brands { get; set; }

          public CarFilterViewModel()
          {
               SelectedBrandId = new List<byte>();
               SelectedVehicleId = new List<byte>();
          }
     }
}
