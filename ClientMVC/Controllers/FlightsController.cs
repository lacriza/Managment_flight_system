using ClientMVC.Models;
using ClientMVC.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
        AddFlightRequestValidatorcs validator = new AddFlightRequestValidatorcs();
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

    public ActionResult Edit(string flightNumber)
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(FlightViewModel flightViewModel)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

  }
}
