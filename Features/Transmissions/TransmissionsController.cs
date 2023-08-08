using CarBookingApp.Features.Transmissions.Command.Create;
using CarBookingApp.Features.Transmissions.Command.Delete;
using CarBookingApp.Features.Transmissions.Command.Edit;
using CarBookingApp.Features.Transmissions.Query.GetAll;
using CarBookingApp.Features.Transmissions.Query.GetById;
using CarBookingApp.Features.Transmissions.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.Transmissions
{
    public class TransmissionsController : Controller
    {
        private readonly IMediator _mediator;

          public TransmissionsController(IMediator mediator)
          {
               _mediator = mediator;
          }

        public async Task<ActionResult> Index()
        {
            var transmissions = await  _mediator.Send(new GetAllTransmissionQuery());

               return View(transmissions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransmissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateTransmissionCommand(model)).Result;
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
                var result = _mediator.Send(new DeleteTransmissionCommand(id)).Result;
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
                var model = await  _mediator.Send(new GetTransmissionByIdQuery(id));

                return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransmissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditTransmissionCommand(model));
            if (result)
            {

                    return RedirectToAction("Success", "Home");
            }

            return View(model);
        }
    }
}
