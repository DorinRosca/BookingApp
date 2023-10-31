using CarBookingApp.Features.FuelTypes.Command.Create;
using CarBookingApp.Features.FuelTypes.Command.Delete;
using CarBookingApp.Features.FuelTypes.Command.Edit;
using CarBookingApp.Features.FuelTypes.Query.GetAll;
using CarBookingApp.Features.FuelTypes.Query.GetById;
using CarBookingApp.Features.FuelTypes.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.FuelTypes
{
    public class FuelTypesController : Controller
    {
        private readonly IMediator _mediator;

          public FuelTypesController(IMediator mediator)
          {
               _mediator = mediator;
          }

        public async Task<ActionResult> Index()
        {
             var fuelTypes = await _mediator.Send(new GetAllFuelTypeQuery());

               return View(fuelTypes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuelTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateFuelTypeCommand(model)).Result;
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
                var result = _mediator.Send(new DeleteFuelTypeCommand(id)).Result;
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
                var model =  await  _mediator.Send(new GetFuelTypeByIdQuery(id));

                return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FuelTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditFuelTypeCommand(model));
            if (result)
            {

                    return RedirectToAction("Success", "Home");
            }

            return View(model);
        }
    }
}
