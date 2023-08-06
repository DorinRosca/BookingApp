using CarBookingApp.Features.Cars.Command.Create;
using CarBookingApp.Features.Cars.Command.Delete;
using CarBookingApp.Features.Cars.Command.Edit;
using CarBookingApp.Features.Cars.Query.GetAll;
using CarBookingApp.Features.Cars.Query.GetById;
using CarBookingApp.Features.Cars.Query.GetDetails;
using CarBookingApp.Features.Cars.Query.GetEditCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarBookingApp.Features.Cars.ViewModel;
using LazyCache;

namespace CarBookingApp.Features.Cars
{
    public class CarsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _appCache;

        public CarsController(IMediator mediator, IAppCache appCache)
        {
             _mediator = mediator;
             _appCache = appCache;
        }
        public ActionResult Index(CarFilterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 var carList =  _mediator.Send(new GetFilteredCarsQuery(model)).Result;

                    return View(carList);
            }
            return RedirectToAction("Error", "Home");
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar(CarCreateViewModel model)
        {
            var itemsToRemove = new List<string> { "Brand", "Vehicle", "Transmission", "Drive", "FuelType", "Image" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateCarCommand(model)).Result;
                if (result)
                {
                     return RedirectToAction("Success", "Home");
                }
            }

            var carDetails = await _appCache.GetOrAddAsync("CarDetails.Get", () => _mediator.Send(new GetCarDetailsQuery()));

               return View("CreateCar", carDetails);
        }
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateCar()
        {
             var carDetails = await _appCache.GetOrAddAsync("CarDetails.Get", () => _mediator.Send(new GetCarDetailsQuery()));
            return View("CreateCar", carDetails);
        }

        public async Task<IActionResult> CarDetails(int id)
        {
            if (ModelState.IsValid)
            {
                    var result = await _mediator.Send(new GetCarByIdQuery(id));
                    return View(result);
            }
            return RedirectToAction("Error", "Home");

        }
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            if (ModelState.IsValid)
            {

                var carDetails = await _mediator.Send(new GetEditCarQuery(id));
                return View(carDetails);
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
                var result = _mediator.Send(new EditCarCommand(model)).Result;
                if (result)
                {

                         return RedirectToAction("Success", "Home");
                }
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

                var result = _mediator.Send(new DeleteCarCommand(id)).Result;
                if (result)
                {

                         return RedirectToAction("Success", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
