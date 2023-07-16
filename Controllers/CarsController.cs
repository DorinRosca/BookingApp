using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace CarBookingApp.Controllers
{
     public class CarsController : Controller
     {
          private readonly ICar _car;

          public CarsController(ICar car)
          {
               this._car = car;
          }
          public ActionResult Index(CarFilterViewModel model = null)
          {
               if (!ModelState.IsValid)
               {

                    var carList = _car.GetFilteredCarList(model).Result;
                    return View(carList);
               }
               return RedirectToAction("Error", "Home");
          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult CreateCar(CarCreateViewModel model)
          {
               var itemsToRemove = new List<string> { "Brand", "Vehicle", "Transmission", "Drive", "FuelType", "Image" };

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    var result = _car.CreateCar(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               
               var carDetails = _car.GetCarDetails().Result;
               return View("CreateCar", carDetails);
          }
          [Authorize(Policy = "Admin")]
          public IActionResult CreateCar()
          {
               var carDetails = _car.GetCarDetails().Result;
               return View("CreateCar", carDetails);
          }

          public IActionResult CarDetails(int id)
          {
               if (ModelState.IsValid)
               {
                    return View(_car.GetSingleCar(id).Result);
               }
               return RedirectToAction("Error", "Home");

          }
          [Authorize(Policy = "Admin")]
          public IActionResult Edit(int id)
          {

               if (ModelState.IsValid)
               {
                    var car = _car.GetEditCar(id).Result;
                    return View(car);
               }
               return RedirectToAction("Error", "Home");

          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult Edit(CarEditViewModel model)
          {
               var itemsToRemove = new List<string> { "Brand", "Vehicle", "Transmission", "Drive", "FuelType", "ImageFile" };
               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    var result = _car.EditCar(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               var invalidFields = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(error => error.ErrorMessage).ToList());

               // Log or display the invalid fields and error messages
               foreach (var field in invalidFields)
               {
                    var fieldName = field.Key;
                    var errorMessages = field.Value;

                    // Log or display the fieldName and errorMessages as needed
                    // For example: Console.WriteLine($"Invalid field '{fieldName}': {string.Join(", ", errorMessages)}");
               }
               return RedirectToAction("Error", "Home");

          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Delete(int id)
          {
               if (ModelState.IsValid)
               {

                    var result = _car.DeleteCar(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               return RedirectToAction("Error", "Home");
          }
     }
}
