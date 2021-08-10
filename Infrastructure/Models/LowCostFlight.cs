using Core.POCO;
using System;

namespace Infrastructure.Models
{
  public class LowCostFlight : Flight
  {
    public LowCostFlight()
    {
    }

    public LowCostFlight(string flightNumber,
      DateTimeOffset departureDateTime,
      DateTimeOffset arrivalDateTime,
      string arrivalAirportIATA,
      string departureAirportIATA,
      decimal basePriceNIS,
      string[] comments,
      PriceOptions options) : base(flightNumber, departureDateTime, arrivalDateTime, arrivalAirportIATA, departureAirportIATA, basePriceNIS)

    {
      Comments = comments;
      TotalPriceNIS = BasePriceNIS * Convert.ToDecimal(options.LowCost);
      FlightType = FlightType.LowCost;
    }

    public override decimal TotalPriceNIS { get; set; }

    public override string[] Comments { get; set; }
  }
}