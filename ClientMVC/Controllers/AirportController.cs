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

 
    public async Task<IActionResult> Dropdown(string fieildForName)
    {
      var airports = await _restHelper.GetIList<AirportModel>("/api/Airport/all");
      var codes = airports.Select(s => new SelectListItem { Value = s.Code, Text = $"[{s.Code}] {s.Name}"});
      var model = new AirportViewModel
      {
        SelectOptions = codes,
        SelectedOption = fieildForName
      };
      return PartialView("_PartialAirportsDropdown", model); ;
    }
  }
}
