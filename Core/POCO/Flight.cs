using System;

namespace Core.POCO
{
  public class Flight
  {
    public string FlightNumber { get; set; }
    public DateTimeOffset DepartureDateTime { get; set; }

    public DateTimeOffset ArrivalDateTime { get; set; }

    public string ArrivalAirportIATA { get; set; }

    public string DepartureAirportIATA { get; set; }

    public FlightType FlightType { get; set; }

    public decimal BasePriceNIS { get; set; }

    public decimal TotalPriceNIS { get; set; }

  }
}
