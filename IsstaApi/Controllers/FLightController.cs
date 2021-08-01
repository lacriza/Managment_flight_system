using AutoMapper;
using Core.POCO;
using FluentValidation.Results;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using IsstaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Validators;


namespace Web.Controllers
{
  [SwaggerTag("Search for flights using amadeus and travelfusion.")]
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class FLightController : ControllerBase
  {

    private readonly IFlightManagmentService _flightService;
    private readonly IMapper _mapper;

    public FLightController(IFlightManagmentService flightService, IMapper mapper)
    {
      _flightService = flightService;
      _mapper = mapper;
    }

    /// <summary>
    /// Return All flights from the database.
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> Get()
    {
      var flights = await _flightService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightResponse>>(flights);

      return Ok(resources);
    }

    /// <summary>
    /// Searches for flights matching the request.
    /// </summary>
    [HttpPost]
    [Route("by-filter-and-page")]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByFilter(Requests.FiltersRequest request)
    {
      FiltersRequestValidator validator = new FiltersRequestValidator();
      ValidationResult results = validator.Validate(request);

      if (!results.IsValid)
      {
        foreach (var failure in results.Errors)
        {
          return BadRequest( "Error: " + failure.ErrorMessage);
        }
      }

      var filters = _mapper.Map<Requests.FiltersRequest, Filters>(request);
      var flights = await _flightService.ListAsync(filters);
      var resources = _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightResponse>>(flights);

      return Ok(resources);
    }


    /// <summary>
    /// Set up new flight.
    /// </summary>
    [HttpPost]
    [Route("add-flight")]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight(Requests.FlightRequest request)
    {
      FlightRequestValidator validator = new FlightRequestValidator();
      ValidationResult results = validator.Validate(request);

      if (!results.IsValid)
      {
        foreach (var failure in results.Errors)
        {
          return BadRequest("Error: " + failure.ErrorMessage);
        }
      }

      var flight = _mapper.Map<Requests.FlightRequest, Flight>(request);
      var flightNumber = await _flightService.AddAsync(flight);
      return Ok($"Flight was added with flight number {flightNumber}");
    }

  }
}
