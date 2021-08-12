using Core.POCO;
using System;
using System.Collections.Generic;

namespace Managment_flight_system.Tests.Common
{
  public static class FlightListCreator
  {
    public static IEnumerable<Flight> CreateFlightList()
    {
      return new List<Flight>()
      {
        new Flight()
        {
          FlightNumber = "PS811",
          DepartureDateTime = DateTimeOffset.Now,
          ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
          ArrivalAirportIATA = "YYZ",
          DepartureAirportIATA = "ODS",
          FlightType = FlightType.Charter,
          BasePriceNIS = 50.00M,
          TotalPriceNIS = 62.50M
        },
        new Flight()
        {
          FlightNumber = "AB445",
          DepartureDateTime = DateTimeOffset.Now,
          ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
          ArrivalAirportIATA = "AAQ",
          DepartureAirportIATA = "BHG",
          FlightType = FlightType.LowCost,
          BasePriceNIS = 100.00M,
          TotalPriceNIS = 90.00M
        },
        new Flight()
        {
          FlightNumber = "FR5126",
          DepartureDateTime = DateTimeOffset.Now,
          ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
          ArrivalAirportIATA = "KBP",
          DepartureAirportIATA = "BCN",
          FlightType = FlightType.Regular,
          BasePriceNIS = 107.00M,
          TotalPriceNIS = 256.00M
        }
      };
    }
  }
}