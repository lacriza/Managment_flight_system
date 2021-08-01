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
      await ValidateAirportsCodesAsync(filters.ToAirportIATACode);
      await ValidateAirportsCodesAsync(filters.FromAirportIATACode);

      return await _flightRepository.GetFiltredPagedFlightsAsync(filters);
    }

    public async Task<string> AddAsync(Flight flight)
    {
      await ValidateAirportsCodesAsync(flight.DepartureAirportIATA);
      await ValidateAirportsCodesAsync(flight.ArrivalAirportIATA);

      flight.FlightNumber = FlightNumberGenerator.Generate(flight.DepartureAirportIATA, flight.ArrivalAirportIATA);
      flight.TotalPriceNIS = flight.BasePriceNIS.CalculateTotalPrice(flight.FlightType);
      await _flightRepository.AddFlightAsync(flight);
      return flight.FlightNumber;
    }

    private async Task ValidateAirportsCodesAsync(string iata) 
    {
      if (!string.IsNullOrEmpty(iata))
      {
        var toIATA = await _airportRepository.GetAirportByIATACodeAsync(iata);
        if (toIATA.Code is null) throw new ArgumentException("Searched Airport Not Exist in DB.");
      }
    }
  }
}
