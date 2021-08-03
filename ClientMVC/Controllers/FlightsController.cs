using ClientMVC.Models;
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

    public async Task<ActionResult> IndexAsync(FiltersRequest filters, PagingRequest paging)
    {
      filters.PagingInfo = paging;
      var flightsPaging = await _restHelper.GetPageList<PagedResponse<FlightViewModel>, FiltersRequest>("/api/FLight/by-filter-and-page", filters);
      return View(flightsPaging);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

  }
}
