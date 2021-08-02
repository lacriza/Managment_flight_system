using IsstaApi.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Requests
{
  public class AddFlightRequest
  {
    /// <summary>
    /// Date and time of outbound flight.
    /// </summary>
    public DateTime DepartureDateTime { get; set; }

    /// <summary>
    /// Date and time of inbound flight.
    /// </summary>
    public DateTime ArrivalDateTime { get; set; }

    /// <summary>
    /// Arrival point: (example: KPB).
    /// </summary>
    public string ArrivalAirportIATA { get; set; }


    /// <summary>
    /// Departure point (example: TLV).
    /// </summary>
    public string DepartureAirportIATA { get; set; }

    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter.
    /// </summary>
    [EnumDataType(typeof(FlightType))]
    public FlightType FlightType { get; set; }

    /// <summary>
    /// Flight price will be calculated automatically based on the base price.
    /// </summary>
    public decimal BasePriceNIS { get; set; }
  }
}
