using Infrastructure.Models;
using System;

namespace Managment_flight_system.Tests.Common
{
  public static class FiltersCreator
  {
    public static Filters CreateDefault() 
    {
      return new Filters
      {
        FromAirportIATACode = "KBP",
        ToAirportIATACode = "LVO",
        FlightType = Core.POCO.FlightType.Regular,
        DateFrom = DateTime.Today,
        DateTo = DateTime.Today.AddDays(1),
        PriceFromInNIS = 0,
        PriceToInNIS = 1000,
        PagingInfo = new PagingInfo
        {
          PageNumber = 1,
          PageSize = 2
        }
      };
    }
  }
}
