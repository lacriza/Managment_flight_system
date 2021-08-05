using ClientMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
  public class AirportController : Controller
  {
    private readonly RESTHelper _restHelper;
    public AirportController(ILogger<AirportController> logger, IConfiguration configuration)
    {
      _restHelper = new RESTHelper(logger, configuration);
    }
    public async Task<IActionResult> Index(AirportViewModel model)
    {
      var airports = await _restHelper.GetIList<AirportModel>("/api/Airport/all");
      var codes = airports.Select(s => new SelectListItem { Value = s.Code, Text = s.Code });
      model.SelectOptions = codes;
      return View(model);
    }
  }
}
