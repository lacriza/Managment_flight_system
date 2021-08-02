using ClientMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace ClientMVC.Controllers
{
  public class FlightsController : Controller
  {
    private readonly ILogger<FlightsController> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public FlightsController(ILogger<FlightsController> logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;
      _apiBaseUrl = _configuration.GetValue<string>("WebAPIBaseUrl");
    }

    public ActionResult Index()
    {
      IEnumerable<FlightViewModel> flights = null;
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(_apiBaseUrl);
        
        //HTTP GET
        var responseTask = client.GetAsync("/api/FLight/all");
        responseTask.Wait();

        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
          var readTask = result.Content.ReadAsAsync<IList<FlightViewModel>>();
          readTask.Wait();

          flights = readTask.Result;
        }
        else //web api sent error response 
        {
          //log response status here..

          flights = Enumerable.Empty<FlightViewModel>();

          ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        }
      }
      return View(flights);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

  }
}
