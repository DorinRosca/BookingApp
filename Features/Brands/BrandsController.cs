﻿using CarBookingApp.Features.Brands.Command.Create;
using CarBookingApp.Features.Brands.Command.Delete;
using CarBookingApp.Features.Brands.Command.Edit;
using CarBookingApp.Features.Brands.Query.GetAll;
using CarBookingApp.Features.Brands.Query.GetById;
using CarBookingApp.Features.Brands.ViewModel;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.Brands
{
    public class BrandsController : Controller
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
             _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var brands = await _mediator.Send(new GetAllBrandQuery());
            return View(brands);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new CreateBrandCommand(model)).Result;
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
                var result = _mediator.Send(new DeleteBrandCommand(id)).Result;
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
                 var model = await _mediator.Send(new GetBrandByIdQuery(id));

                 return View(model);
            }
            return RedirectToAction("Error", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new EditBrandCommand(model));
            if (result)
            {
                 return RedirectToAction("Success", "Home");
            }

            return View(model);
        }
    }
}