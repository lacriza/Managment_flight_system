using ClientMVC.Models;
using ClientMVC.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
    
    public async Task<ActionResult> Index(int? page = null)
    {
      try
      {
        var filters = new FiltersRequest()
        {
          PagingInfo = new PagingRequest()
          {
            PageNumber = (page == null) ? 1 : page.Value
          }
        };
        var flightsPaging = await _restHelper.POST<PagedResponse<FlightViewModel>, FiltersRequest>("/api/FLight/by-filter-and-page", filters);
        return View(flightsPaging.Data);
      }
      catch (Exception e)
      {
        return RedirectToAction(nameof(Error), e.Message);
      }
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
      catch (Exception e)
      {
        return RedirectToAction(nameof(Error), e.Message);
      }
    }

    public async Task<ActionResult> EditAsync(string id)
    {
      var all = await _restHelper.GET<FlightViewModel>("/api/FLight/all");
      var flightForEdit = all.FirstOrDefault(f => f.FlightNumber == id);
      return PartialView("Edit", flightForEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(FlightViewModel flightViewModel)
    {
      try
      {
        if (!ModelState.IsValid) 
        {
          return PartialView("Edit", flightViewModel);
        }
          var response = await _restHelper.PUT<FlightViewModel, FlightViewModel>("/api/FLight/update-flight", flightViewModel);
          return RedirectToAction(nameof(Index));
      }
      catch (Exception e)
      {
        return RedirectToAction(nameof(Error), e.Message);
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message)
    {
      return View(new ErrorViewModel 
      { 
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        Message = message
      });
    }
  }
}