using CarBookingApp.Features.Roles.Query.GetAll;
using CarBookingApp.Features.Statuses.Command.Create;
using CarBookingApp.Features.Statuses.Command.Delete;
using CarBookingApp.Features.Statuses.Command.Edit;
using CarBookingApp.Features.Statuses.Query.GetAll;
using CarBookingApp.Features.Statuses.Query.GetById;
using CarBookingApp.Features.Statuses.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.Statuses
{
    public class StatusController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _appCache;

          public StatusController(IMediator mediator, IAppCache appCache)
          {
               _mediator = mediator;
               _appCache = appCache;
          }
        public async Task<ActionResult> Index()
        {

            var result = await _appCache.GetOrAddAsync("Statuses.Get", () => _mediator.Send(new GetAllStatusQuery()),DateTime.Now.AddHours(4));

               return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StatusViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateStatusCommand(model));
                if (result)
                {
                     return RedirectToAction("Success", "Home");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(byte id)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new DeleteStatusCommand(id));
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
                var model = await _mediator.Send(new GetStatusByIdQuery(id));

                return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditStatusCommand(model));
            if (result)
            {

                    return RedirectToAction("Success", "Home");
            }

            return View(model);
        }

    }
}
