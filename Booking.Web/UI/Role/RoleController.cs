using Booking.Application.Features.Role;
using Booking.Application.Features.Role.Commands.Add;
using Booking.Application.Features.Role.Commands.Delete;
using Booking.Application.Features.Role.Commands.Update;
using Booking.Application.Features.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Web.UI.Role
{
     [Authorize(Roles = "Admin")]
     public class RoleController : Controller
     {
          private readonly IMediator _mediator;

          public RoleController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getRolesQuery = new GetRolesQuery();
               var roles = await _mediator.Send(getRolesQuery);
               return View(roles);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(RoleModel roleModel)
          {
               var addRoleCommand = new AddRoleCommand(roleModel);
               var insertedResult = await _mediator.Send(addRoleCommand);
               return View(insertedResult);
          }

          [HttpPost]
          public async Task<ActionResult> Delete(string roleId)
          {
               var deleteRoleCommand = new DeleteRoleCommand(roleId);
               var deleteResult = await _mediator.Send(deleteRoleCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }
               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(string roleId)
          {
               var getRoleQuery = new GetRoleQuery(roleId);
               var roleToUpdate = await _mediator.Send(getRoleQuery);

               if (roleToUpdate != null)
               {
                    var data = new Tuple<UpdateRoleResponse?, RoleModel>(null, roleToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(RoleModel updatedRoleModel)
          {
               var updateRoleCommand = new UpdateRoleCommand(updatedRoleModel);
               var updateResult = await _mediator.Send(updateRoleCommand);

               var data = new Tuple<UpdateRoleResponse, RoleModel?>(updateResult, updatedRoleModel);
               return View(data);
          }
     }
}
