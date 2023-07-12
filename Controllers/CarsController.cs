using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace CarBookingApp.Controllers
{
     public class CarsController : Controller
     {
          private readonly ICar _car;

          public CarsController(ICar car)
          {
               this._car = car;
          }
          public ActionResult Index()
          {
               var carList = _car.GetAllAvailableCars();
               return View(carList.Result);
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult CreateCar(CarCreateViewModel model)
          {
               var result = _car.CreateCar(model).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }
               return RedirectToAction("Error", "Home");
          }

          public IActionResult CreateCar()
          {
               var carDetails = _car.GetCarDetails();
               return View(carDetails);
          }

          public IActionResult CarDetails(int id)
          {
               return View(_car.GetSingleCar(id).Result);
          }

          public IActionResult CarFilter()
          {
               return View();
          }

          public IActionResult Edit(int id)
          {
               var car = _car.GetEditCar(id).Result;
               return View(car);
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult Edit(CarEditViewModel model)
          {
               var result = _car.EditCar(model).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }
               return RedirectToAction("Error", "Home");

          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Delete(int id)
          {
               var result = _car.DeleteCar(id).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return RedirectToAction("Error", "Home");
          }
     }
}
