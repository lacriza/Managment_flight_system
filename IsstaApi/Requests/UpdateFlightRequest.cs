using IsstaApi.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Requests
{
  public class UpdateFlightRequest
  {
    /// <summary>
    /// Flight Id. Required.
    /// </summary>
    public string FlightNumber { get; set; }

    /// <summary>
    /// Date and time of outbound flight. Optional.
    /// </summary>
    public DateTime? DepartureDateTime { get; set; }

    /// <summary>
    /// Date and time of inbound flight. Optional.
    /// </summary>
    public DateTime? ArrivalDateTime { get; set; }

    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter. Optional.
    /// </summary>
    [EnumDataType(typeof(FlightType))]
    public FlightType? FlightType { get; set; }

    /// <summary>
    /// Flight price will be calculated automatically based on the base price. Optional.
    /// </summary>
    public decimal? BasePriceNIS { get; set; }
  }
}
