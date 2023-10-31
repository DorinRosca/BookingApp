using CarBookingApp.Features.Brands.Entities;
using CarBookingApp.Features.Cars.ViewModel;
using CarBookingApp.Features.Vehicles.Entities;
using MediatR;

namespace CarBookingApp.Features.Cars.Query.GetAll
{
    public record GetFilteredCarsQuery(CarFilterViewModel model) : IRequest<CarFilterViewModel>
     {
          public List<CarViewModel> Cars = model.Cars;
          //selected filter
          public List<byte> SelectedVehicleId = model.SelectedVehicleId;

          public List<byte> SelectedBrandId = model.SelectedBrandId;
          //filters
          public string SearchCarName = model.SearchCarName;
          public List<Vehicle> Vehicles = model.Vehicles;
          public List<Brand> Brands = model.Brands;

     }
}
