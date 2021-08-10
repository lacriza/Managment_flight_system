using Core.POCO;
using System;

namespace Infrastructure.Models
{
  public class Flight
  {
    public Flight()
    {

    }
    public Flight(string flightNumber, DateTimeOffset departureDateTime, DateTimeOffset arrivalDateTime, string arrivalAirportIATA, string departureAirportIATA, decimal basePriceNIS)
    {
      FlightNumber = flightNumber;
      DepartureDateTime = departureDateTime;
      ArrivalDateTime = arrivalDateTime;
      ArrivalAirportIATA = arrivalAirportIATA;
      DepartureAirportIATA = departureAirportIATA;
      BasePriceNIS = basePriceNIS;
    }

    public string FlightNumber { get; set; }
    public DateTimeOffset DepartureDateTime { get; set; }

    public DateTimeOffset ArrivalDateTime { get; set; }

    public string ArrivalAirportIATA { get; set; }

    public string DepartureAirportIATA { get; set; }
    public decimal BasePriceNIS { get; set; }

    public FlightType FlightType { get; set; }

    public virtual decimal TotalPriceNIS { get; set; }

    public virtual string[] Comments { get; set; }
  }
}
