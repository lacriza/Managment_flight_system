using AutoMapper;
using Core.POCO;
using Infrastructure.Interfaces;
using IsstaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
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

    // GET: api/<FLightController>
    [HttpGet]
    public async Task<IEnumerable<FlightResponse>> Get()
    {
      var categories = await _flightService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightResponse>>(categories);

      return resources;
    }

    // POST api/<FLightController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<FLightController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

  }
}
