using IsstaApi.Enums;
using System;

namespace IsstaApi.Models
{
  public class FlightResponse
  {
    public string FlightNumber { get; set; }
    public DateTimeOffset DepartureDateTime { get; set; }

    public DateTimeOffset ArrivalDateTime { get; set; }

    public string ArrivalAirportIATA { get; set; }

    public string DepartureAirportIATA { get; set; }

    public FlightType FlightType { get; set; }

    public decimal TotalPriceNIS { get; set; }

    public decimal BasePriceNIS { get; set; }

    public string[] Comments { get; set; }
  }
}
