﻿using AutoMapper;
using Core.POCO;
using FluentValidation.Results;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using IsstaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Requests;
using Web.Validators;


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
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    public async Task<IActionResult> GetByFilter(FiltersRequest request)
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

      var filters = _mapper.Map<FiltersRequest, Filters>(request);
      var flights = await _flightService.ListAsync(filters);

      return Ok(flights);
    }


    /// <summary>
    /// Set up new flight.
    /// </summary>
    [HttpPost]
    [Route("add-flight")]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight(AddFlightRequest request)
    {
      AddFlightRequestValidator validator = new AddFlightRequestValidator();
      ValidationResult results = validator.Validate(request);

      if (!results.IsValid)
      {
        foreach (var failure in results.Errors)
        {
          return BadRequest("Error: " + failure.ErrorMessage);
        }
      }

      var flight = _mapper.Map<AddFlightRequest, Flight>(request);
      var flightNumber = await _flightService.AddAsync(flight);
      return Ok($"Flight added with flight number {flightNumber}");
    }

    /// <summary>
    /// Update existing flight.
    /// </summary>
    [HttpPut]
    [Route("update-flight")]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateFlightRequest request)
    {
      UpdateFlightRequestValidator validator = new UpdateFlightRequestValidator();
      ValidationResult results = validator.Validate(request);

      if (!results.IsValid)
      {
        foreach (var failure in results.Errors)
        {
          return BadRequest("Error: " + failure.ErrorMessage);
        }
      }

      var flight = _mapper.Map<UpdateFlightRequest, Flight>(request);
      
      await _flightService.UpdateAsync(flight);
      return Ok($"Flight with flight number {request.FlightNumber} updated ");
    }

}
}
