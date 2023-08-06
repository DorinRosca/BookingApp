using CarBookingApp.Features.Vehicles.Command.Create;
using CarBookingApp.Features.Vehicles.Command.Delete;
using CarBookingApp.Features.Vehicles.Command.Edit;
using CarBookingApp.Features.Vehicles.Query.GetAll;
using CarBookingApp.Features.Vehicles.Query.GetById;
using CarBookingApp.Features.Vehicles.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.Vehicles
{
    public class VehicleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _appCache;

          public VehicleController(IMediator mediator, IAppCache appCache)
          {
               _mediator = mediator;
               _appCache = appCache;
          }

        public async Task<ActionResult> Index()
        {
             var vehicles = await _appCache.GetOrAddAsync("Vehicles.Get", () => _mediator.Send(new GetAllVehicleQuery()),DateTime.Now.AddHours(4));

            return View(vehicles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateVehicleCommand(model)).Result;
                if (result)
                {
                     return RedirectToAction("Success", "Home");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(byte id)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new DeleteVehicleCommand(id)).Result;
                if (result)
                {
                     return RedirectToAction("Success", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<ActionResult> Edit(byte id)
        {
            if (ModelState.IsValid)
            {
                 var model = await _mediator.Send(new GetVehicleByIdQuery(id));

                 return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditVehicleCommand(model));
            if (result)
            {

                    return RedirectToAction("Success", "Home");
            }

            return View(model);
        }
    }
}
