using Car_Booking_App.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class CarFilterViewModel
     {
          public List<CarViewModel> Cars { get; set; }
          //selected filter
          public byte SelectedVehicleId { get; set; }
          public byte SelectedBrandId { get; set; }
          //filters

          public List<Vehicle> Vehicles { get; set; }
          public List<Brand> Brands { get; set; }
     }
}
