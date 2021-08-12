using Core.POCO;
using System;

namespace Infrastructure.Models
{
  public class CharterFlight: Flight
  {
    public CharterFlight()
    {
    }

    public CharterFlight(string flightNumber,
      DateTimeOffset departureDateTime,
      DateTimeOffset arrivalDateTime,
      string arrivalAirportIATA,
      string departureAirportIATA,
      decimal basePriceNIS,
      string[] comments,
      PriceOptions options) : base(flightNumber, departureDateTime, arrivalDateTime, arrivalAirportIATA, departureAirportIATA, basePriceNIS)

    {
      Comments = comments;
      TotalPriceNIS = (BasePriceNIS * Convert.ToDecimal(options.Charter)) + Convert.ToDecimal(options.CharterFixed);
      FlightType = FlightType.Charter;
    }

    public override decimal TotalPriceNIS { get; set; }

    public override string[] Comments { get; set; }
  }
}
