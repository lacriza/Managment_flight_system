using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
  public class FLightManagmentService : IFlightManagmentService
  {
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;

    public FLightManagmentService(IFlightRepository flightRepository, IAirportRepository airportRepository)
    {
      _flightRepository = flightRepository;
      _airportRepository = airportRepository;
    }

    public async Task<IEnumerable<Flight>> ListAsync()
    {
        return await _flightRepository.GetAllFlightsAsync();
    }

    public async Task<IEnumerable<Flight>> ListAsync(Filters filters)
    {
      if (!string.IsNullOrEmpty(filters.ToAirportIATACode)) 
      {
        var toIATA = await _airportRepository.GetAirportByIATACodeAsync(filters.ToAirportIATACode);
        if (toIATA.Code is null) throw new ArgumentException("Searched Airport Not Exist in DB.");
      }

      if (!string.IsNullOrEmpty(filters.FromAirportIATACode))
      {
        var fromIATA = await _airportRepository.GetAirportByIATACodeAsync(filters.FromAirportIATACode);
        if (fromIATA.Code is null) throw new ArgumentException("Searched Airport Not Exist in DB.");
      }

      return await _flightRepository.GetFiltredPagedFlights(filters);
    }
  }
}
