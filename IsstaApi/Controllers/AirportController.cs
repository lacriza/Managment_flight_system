using AutoMapper;
using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using IsstaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Requests;

namespace Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AirportController : ControllerBase
  {

    private readonly IAirportRepository _airportRepository;
    private readonly IMapper _mapper;

    public AirportController(IAirportRepository airportRepository, IMapper mapper)
    {
      _airportRepository = airportRepository;
      _mapper = mapper;
    }

    /// <summary>
    /// Return all airports from the database.
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> Get()
    {
      var airports = await _airportRepository.GetAllAirportsAsync();
      var resources = _mapper.Map<IEnumerable<Airport>, IEnumerable<AirportResponse>>(airports);

      return Ok(resources);
    }

    /// <summary>
    /// Searches for airport matching the code.
    /// </summary>
    [HttpPost]
    [Route("by-code")]
    [ProducesResponseType(typeof(AirportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByCode(string code)
    {
      if (string.IsNullOrEmpty(code) || code.Length > 3 || code.Length < 3) 
      {
        return BadRequest("Airport code must be 3 character long: ");
      }

      var airport = await _airportRepository.GetAirportByIATACodeAsync(code);
      var resources = _mapper.Map<Airport, AirportResponse>(airport);

      return Ok(resources);
    }

    /// <summary>
    /// Return page of airports from the database corresponding page request.
    /// </summary>
    [HttpPost]
    [Route("by-page")]
    [ProducesResponseType(typeof(AirportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByFilter(PagingRequest request)
    {
      var paging = _mapper.Map<PagingRequest, PagingInfo>(request);
      var airports = await _airportRepository.GetPagedAirports(paging);
      var resources = _mapper.Map<IEnumerable<Airport>, IEnumerable<AirportResponse>>(airports);

      return Ok(resources);
    }
  }
}
