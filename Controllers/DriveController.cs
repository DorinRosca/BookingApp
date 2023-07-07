using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Controllers
{
     public class DriveController : Controller
     {
          private readonly IDrive _drive;

          public DriveController(IDrive drive)
          {
               this._drive=drive;
          }
          // GET: DriveController
          public ActionResult Index()
          {
               var drives = _drive.GetAllDrives();
               return View(drives.Result);
          }

          // GET: DriveController/Create
          public ActionResult Create()
          {
               return View();
          }

          // POST: DriveController/Create
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Create(DriveViewModel model)
          {
               var result = _drive.AddDrive(model).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }
               return RedirectToAction("Error", "Home");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public  ActionResult Delete(int id)
          {
               var result =  _drive.DeleteDrive(id).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }
               return RedirectToAction("Error", "Home");
          }

          public ActionResult Edit(int id)
          {
               // Retrieve the DriveViewModel from the database using the provided ID
               DriveViewModel model = _drive.GetDrive(id).Result;
               if (model == null)
               {
                    // Handle the case where the model with the specified ID was not found
                    return RedirectToAction("Error", "Home");
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Edit(DriveViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    // If the model state is not valid, return the view with the validation errors
                    return View(model);
               }

               var result = _drive.EditDrive(model).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return RedirectToAction("Error", "Home");
          }

     }
}
