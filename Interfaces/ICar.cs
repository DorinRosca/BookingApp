using CarBookingApp.ViewModels;

namespace CarBookingApp.Interfaces
{
     public interface ICar
     {
          public CarCreateViewModel GetCarDetails();
          public Task<bool> CreateCar(CarCreateViewModel model);
          public Task<IEnumerable<CarViewModel>> GetAllAvailableCars();
          public Task<CarViewModel> GetSingleCar(int id);
          public Task<CarEditViewModel> GetEditCar(int id);

          public Task<bool> EditCar(CarEditViewModel viewmodel);

          public Task<bool> DeleteCar(int id);


     }
}
