using CarBookingApp.Features.Cars.Query.GetAll;
using CarBookingApp.Features.Roles.Command.Create;
using CarBookingApp.Features.Roles.Command.Delete;
using CarBookingApp.Features.Roles.Command.Edit;
using CarBookingApp.Features.Roles.Query.GetAll;
using CarBookingApp.Features.Roles.Query.GetById;
using CarBookingApp.Features.Roles.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Features.Roles
{
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _appCache;

          public RoleController(IMediator mediator, IAppCache appCache)
          {
               _mediator = mediator;
               _appCache = appCache;
          }
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Index()
        {
            var roles = await _appCache.GetOrAddAsync("Roles.Get", () => _mediator.Send(new GetAllRoleQuery()),DateTime.Now.AddHours(4));

               return View(roles);
        }
        [Authorize(Policy = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel model)
        {
            var itemsToRemove = new List<string> { "NormalizedName", "Id" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateRoleCommand(model)).Result;
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
        public ActionResult Delete(string id)
        {
            var itemsToRemove = new List<string> { "NormalizedName", "Id" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new DeleteRoleCommand(id)).Result;
                if (result)
                {
                     return RedirectToAction("Success", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var itemsToRemove = new List<string> { "NormalizedName", "Id" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetRoleByIdQuery(id));

                    if (model == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(model);

            }

            return RedirectToAction("Error", "Home");

        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            var itemsToRemove = new List<string> { "NormalizedName", "Id" };

            foreach (var item in itemsToRemove)
            {
                ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new EditRoleCommand(model));
                if (result)
                {

                         return RedirectToAction("Success", "Home");
                }
            }

            return View(model);
        }
    }
}
