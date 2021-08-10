using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<PagedResponse<List<Flight>>> ListAsync(Filters filters)
    {
      await ValidateAirportsCodesAsync(filters.ToAirportIATACode);
      await ValidateAirportsCodesAsync(filters.FromAirportIATACode);

      var list = await _flightRepository.GetFiltredPagedFlightsAsync(filters);
      var totalRecords = list.Item2;
      var pagedList = list.Item1.ToList().CreatePagedReponse<Flight>(filters.PagingInfo.PageNumber, filters.PagingInfo.PageSize, totalRecords);
      return pagedList;
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
        var toIATA = await _airportRepository.GetById(iata);
        if (toIATA.Code is null) throw new ArgumentException("Searched Airport Not Exist in DB.");
      }
    }

    public async Task<Flight> UpdateAsync(Flight flight)
    {
      var flightForUpdating = await _flightRepository.GetByIdAsync(flight.FlightNumber);

      if (flightForUpdating == null)
        throw new ArgumentException("This Flight Number does not exist in DB.");

      if (flight.DepartureDateTime != default) 
      {
        flightForUpdating.DepartureDateTime = flight.DepartureDateTime;
      }

      if (flight.ArrivalDateTime != default)
      {
        flightForUpdating.DepartureDateTime = flight.ArrivalDateTime;
      }

      if (flight.FlightType != flightForUpdating.FlightType)
      {
        flightForUpdating.FlightType = flight.FlightType;
      }

      if (flight.BasePriceNIS != default && flight.BasePriceNIS != flightForUpdating.BasePriceNIS)
      {
        flightForUpdating.BasePriceNIS = flight.BasePriceNIS;       
      }

      flight.TotalPriceNIS = flightForUpdating.BasePriceNIS.CalculateTotalPrice(flight.FlightType);
      return await _flightRepository.UpdateByIdAsync(flight);
    }
  }
}
