using AutoMapper;
using Core.POCO;
using FluentValidation.Results;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using IsstaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Web.MapperProfile;
using Web.Requests;
using Web.Validators;
using Flight = Infrastructure.Models.Flight;

namespace Web.Controllers
{
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
      var flights = await _flightService.ListAsync();
      return Ok(flights);
    }

    /// <summary>
    /// Searches for flights matching the request.
    /// </summary>
    [HttpPost]
    [Route("by-filter-and-page")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByFilter(FiltersRequest request)
    {
      request.PriceRangeMap();
      FiltersRequestValidator validator = new FiltersRequestValidator();
      ValidationResult results = validator.Validate(request);

      var validate = IsOK(results);
      if (validate.Item1)
      {
        var filters = _mapper.Map<FiltersRequest, Filters>(request);
        var flights = await _flightService.ListAsync(filters);
    
         return Ok(flights);
      }

      return BadRequest(validate.Item2);  
    }



    /// <summary>
    /// Set up new flight.
    /// </summary>
    [HttpPost]
    [Route("add-flight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight(AddFlightRequest request)
    {
      AddFlightRequestValidator validator = new AddFlightRequestValidator();
      ValidationResult results = validator.Validate(request);
      var validate = IsOK(results);

      if (validate.Item1)
      {
        var flight = _mapper.Map<AddFlightRequest, Flight>(request);
        var flightNumber = await _flightService.AddAsync(flight);
        return Ok($"Flight added with flight number {flightNumber}");
      }

      return BadRequest(validate.Item2);
    }

    /// <summary>
    /// Update existing flight.
    /// </summary>
    [HttpPut]
    [Route("update-flight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateFlightRequest request)
    {
      UpdateFlightRequestValidator validator = new UpdateFlightRequestValidator();
      ValidationResult results = validator.Validate(request);
      var validate = IsOK(results);

      if (validate.Item1) 
      {
        var flight = _mapper.Map<UpdateFlightRequest, Flight>(request);
        await _flightService.UpdateAsync(flight);
        return Ok(flight);
      }

      return BadRequest(validate.Item2);
    }


    private (bool, Dictionary<string, string>) IsOK(ValidationResult results) 
    {
      if (!results.IsValid)
      {
        var errors = new Dictionary<string, string>();
        foreach (var failure in results.Errors)
        {
          errors.Add(failure.PropertyName, failure.ErrorMessage);
        }
        return (false, errors);
      }
      return (true, null);
    }

  }
}
