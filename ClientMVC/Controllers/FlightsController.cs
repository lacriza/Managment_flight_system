﻿using ClientMVC.Models;
using ClientMVC.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
  public class FlightsController : Controller
  {
    private readonly RESTHelper _restHelper;

    public FlightsController(ILogger<FlightsController> logger, IConfiguration configuration)
    {
      _restHelper = new RESTHelper(logger, configuration);
    }

    public async Task<ActionResult> Index()
    {
      var filters = new FiltersRequest()
      {
        PagingInfo = new PagingRequest()
      };
      var flightsPaging = await _restHelper.POST<PagedResponse<FlightViewModel>, FiltersRequest>("/api/FLight/by-filter-and-page", filters);
      return View(flightsPaging.Data);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<ActionResult> Index(FiltersRequest filters, PagingRequest paging)
    {
      filters.PagingInfo = paging;
      var flightsPaging = await _restHelper.POST<PagedResponse<FlightViewModel>, FiltersRequest>("/api/FLight/by-filter-and-page", filters);
      return View(flightsPaging.Data);
    }

    // GET: FlightsController/Add
    public ActionResult Add()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<ActionResult> Add(AddFlightViewModel flight)
    {
      try
      {
        AddFlightValidator validator = new AddFlightValidator();
        ValidationResult results = validator.Validate(flight);
        if (!results.IsValid)
        {
          foreach (var error in results.Errors)
          {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
          }
          return View();
        }
        var message = await _restHelper.POST<string, AddFlightViewModel>("/api/FLight/add-flight", flight);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    public async Task<ActionResult> EditAsync(string id)
    {
      var all = await _restHelper.GetIList<FlightViewModel>("/api/FLight/all");
      var flightForEdit = all.FirstOrDefault(f => f.FlightNumber == id);
      return PartialView("Edit", flightForEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(FlightViewModel flightViewModel)
    {
      try
      {
        var flight = await _restHelper.PUT<FlightViewModel, FlightViewModel>("/api/FLight/update-flight", flightViewModel);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return RedirectToAction(nameof(Index));
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}