using AutoMapper;
using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flight = Infrastructure.Models.Flight;

namespace Infrastructure.Services
{
  public class FLightManagmentService : IFlightManagmentService
  {
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;
    private readonly IRepository<Comment> _commentsRepository;
    private readonly PriceOptions _options;
    private readonly IMapper _mapper;

    public FLightManagmentService(
      IFlightRepository flightRepository,
      IAirportRepository airportRepository,
      IRepository<Comment> commentsRepository,
      IOptions<PriceOptions> options, IMapper mapper)
    {
      _options = options.Value;
      _flightRepository = flightRepository;
      _airportRepository = airportRepository;
      _commentsRepository = commentsRepository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<Flight>> ListAsync()
    {
      var list = new List<Flight>();
      var flights = await _flightRepository.GetAll();
      var commentsList = await _commentsRepository.GetAll();
      foreach (var flight in flights)
      {
        var comments = commentsList.Where(c => c.FlightType == flight.FlightType).Select(s => s.Text).ToArray();
        Flight mappedflight = MapType(flight.FlightType, flight, comments);
        list.Add(mappedflight);
      }
      return list;
    }

    public async Task<PagedResponse<List<Flight>>> ListAsync(Filters filters)
    {
      await ValidateAirportsCodesAsync(filters.ToAirportIATACode);
      await ValidateAirportsCodesAsync(filters.FromAirportIATACode);
      var list = new List<Flight>();
      var flights = await _flightRepository.GetFiltredPagedFlightsAsync(filters);
      var commentsList = await _commentsRepository.GetAll();
      foreach (var flight in flights.Item1)
      {
        var comments = commentsList.Where(c => c.FlightType == flight.FlightType).Select(s => s.Text).ToArray();
        Flight mappedflight = MapType(flight.FlightType, flight, comments);
        list.Add(mappedflight);
      }
      var totalRecords = flights.Item2;
      var pagedList = list.CreatePagedReponse(filters.PagingInfo.PageNumber, filters.PagingInfo.PageSize, totalRecords);
      return pagedList;
    }


    public async Task<string> AddAsync(Flight flight)
    {
      await ValidateAirportsCodesAsync(flight.DepartureAirportIATA);
      await ValidateAirportsCodesAsync(flight.ArrivalAirportIATA);

      flight.FlightNumber = FlightNumberGenerator.Generate(flight.DepartureAirportIATA, flight.ArrivalAirportIATA);
      var pocoFlight = _mapper.Map<Flight, Core.POCO.Flight>(flight);
      Flight flightBLL = MapType(flight.FlightType, pocoFlight, null);

      var resourse = _mapper.Map<Flight, Core.POCO.Flight>(flightBLL);
      await _flightRepository.Insert(resourse);
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
      var flightForUpdating = await _flightRepository.GetById(flight.FlightNumber);

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

      var result = await _flightRepository.Update(flightForUpdating);
      return flight;
    }

    private Flight MapType(FlightType type, Core.POCO.Flight flight, string[] comments)
    {
      return type switch
      {
        FlightType.Regular =>
        new RegularFlight(flight.FlightNumber,
                          flight.DepartureDateTime,
                          flight.ArrivalDateTime,
                          flight.ArrivalAirportIATA,
                          flight.DepartureAirportIATA,
                          flight.BasePriceNIS,
                          comments,
                          _options),
        FlightType.LowCost => new LowCostFlight(flight.FlightNumber,
                          flight.DepartureDateTime,
                          flight.ArrivalDateTime,
                          flight.ArrivalAirportIATA,
                          flight.DepartureAirportIATA,
                          flight.BasePriceNIS,
                          comments,
                          _options),
        FlightType.Charter => new CharterFlight(flight.FlightNumber,
                          flight.DepartureDateTime,
                          flight.ArrivalDateTime,
                          flight.ArrivalAirportIATA,
                          flight.DepartureAirportIATA,
                          flight.BasePriceNIS,
                          comments,
                          _options),
        _ => new RegularFlight(),
      };
    }
  }
}