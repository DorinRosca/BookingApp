using CarBookingApp.Features.Drives.Command.Create;
using CarBookingApp.Features.Drives.Command.Delete;
using CarBookingApp.Features.Drives.Command.Edit;
using CarBookingApp.Features.Drives.Query.GetAll;
using CarBookingApp.Features.Drives.Query.GetById;
using CarBookingApp.Features.Drives.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Features.Drives
{
    public class DrivesController : Controller
    {
        private readonly IMediator _mediator;
          
          public DrivesController(IMediator mediator)
          {
               _mediator = mediator;
          }

        public async Task<ActionResult> Index()
        {
            var drives = await _mediator.Send(new GetAllDriveQuery());

               return View(drives);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateDriveCommand(model)).Result;
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
                var result = _mediator.Send(new DeleteDriveCommand(id)).Result;
                if (result)
                {
                         return RedirectToAction("Success","Home");
                }
            }
            return RedirectToAction("Error","Home");
        }

        public async Task<ActionResult> Edit(byte id)
        {
            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetDriveByIdQuery(id));

                return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DriveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditDriveCommand(model));
            if (result)
            {
                    return RedirectToAction("Success", "Home");
            }

            return View(model);
        }
    }
}
