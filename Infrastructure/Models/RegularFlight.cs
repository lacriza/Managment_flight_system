using Core.POCO;
using System;

namespace Infrastructure.Models
{
  public class RegularFlight: Flight
  {
    public RegularFlight()
    {
    }

    public RegularFlight(
      string flightNumber,
      DateTimeOffset departureDateTime,
      DateTimeOffset arrivalDateTime,
      string arrivalAirportIATA,
      string departureAirportIATA,
      decimal basePriceNIS,
      string[] comments,
      PriceOptions options) : base(flightNumber, departureDateTime, arrivalDateTime, arrivalAirportIATA, departureAirportIATA, basePriceNIS)
    {
      Comments = comments;
      TotalPriceNIS = BasePriceNIS * Convert.ToDecimal(options.Regular);
      FlightType = FlightType.Regular;
    }

    public override decimal TotalPriceNIS { get; set; }

    public override string[] Comments { get; set; }
  }
}
