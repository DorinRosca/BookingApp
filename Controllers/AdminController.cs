using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Controllers
{
     [Authorize(Policy = "Admin")]
     public class AdminController : Controller
     {
          private readonly IAdmin _admin;

          public AdminController(IAdmin drive)
          {
               this._admin = drive;
          }

          public ActionResult Drives()
          {
               var drives = _admin.GetAllDrive();
               return View(drives.Result);
          }

          public ActionResult CreateDrive()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateDrive(DriveViewModel model)
          {
               if (ModelState.IsValid)
               {

                    var result = _admin.AddDrive(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteDrive(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteDrive(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");

          }

          public ActionResult EditDrive(byte id)
          {
               if (ModelState.IsValid)
               {

                    DriveViewModel model = _admin.GetDrive(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditDrive(DriveViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditDrive(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }


          public ActionResult Brands()
          {
               var brands = _admin.GetAllBrand();
               return View(brands.Result);
          }

          public ActionResult CreateBrand()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateBrand(BrandViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.AddBrand(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteBrand(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteBrand(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               return RedirectToAction("Error", "Home");
          }

          public ActionResult EditBrand(byte id)
          {
               if (ModelState.IsValid)
               {
                    BrandViewModel model = _admin.GetBrand(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditBrand(BrandViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditBrand(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }

          public ActionResult FuelTypes()
          {
               var fuelTypes = _admin.GetAllFuelType();
               return View(fuelTypes.Result);
          }

          public ActionResult CreateFuelType()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateFuelType(FuelTypeViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.AddFuelType(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteFuelType(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteFuelType(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }

          public ActionResult EditFuelType(byte id)
          {
               if (ModelState.IsValid)
               {
                    FuelTypeViewModel model = _admin.GetFuelType(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditFuelType(FuelTypeViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditFuelType(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }

          public ActionResult Transmissions()
          {
               var trabsmission = _admin.GetAllTransmission();
               return View(trabsmission.Result);
          }

          public ActionResult CreateTransmission()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateTransmission(TransmissionViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.AddTransmission(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteTransmission(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteTransmission(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }

          public ActionResult EditTransmission(byte id)
          {
               if (ModelState.IsValid)
               {
                    TransmissionViewModel model = _admin.GetTransmission(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditTransmission(TransmissionViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditTransmission(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }

          public ActionResult Vehicles()
          {
               var vehicles = _admin.GetAllVehicle();
               return View(vehicles.Result);
          }

          public ActionResult CreateVehicle()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateVehicle(VehicleViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.AddVehicle(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteVehicle(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteVehicle(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               return RedirectToAction("Error", "Home");
          }

          public ActionResult EditVehicle(byte id)
          {
               if (ModelState.IsValid)
               {
                    VehicleViewModel model = _admin.GetVehicle(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditVehicle(VehicleViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditVehicle(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }
          public ActionResult Status()
          {
               var vehicles = _admin.GetAllStatus();
               return View(vehicles.Result);
          }

          public ActionResult CreateStatus()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateStatus(StatusViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.AddStatus(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteStatus(byte id)
          {
               if (ModelState.IsValid)
               {
                    var result = _admin.DeleteStatus(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }

          public ActionResult EditStatus(byte id)
          {
               if (ModelState.IsValid)
               {
                    StatusViewModel model = _admin.GetStatus(id).Result;
                    if (model == null)
                    {
                         return RedirectToAction("Error", "Home");
                    }

                    return View(model);
               }
               return RedirectToAction("Error", "Home");

          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditStatus(StatusViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _admin.EditStatus(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return View(model);
          }

     }
}
