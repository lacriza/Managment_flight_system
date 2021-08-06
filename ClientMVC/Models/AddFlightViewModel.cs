using System;
using System.ComponentModel.DataAnnotations;

namespace ClientMVC.Models
{
  public class AddFlightViewModel
  {
    /// <summary>
    /// Date and time of outbound flight.
    /// </summary>
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDateTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    /// <summary>
    /// Date and time of inbound flight.
    /// </summary>
    public DateTime ArrivalDateTime { get; set; }

    /// <summary>
    /// Arrival point: (example: KPB).
    /// </summary>
    [Required]
    [StringLength(3)]
    public string ArrivalAirportIATA { get; set; }

    /// <summary>
    /// Departure point (example: TLV).
    /// </summary>
    [Required]
    [StringLength(3)]
    public string DepartureAirportIATA { get; set; }

    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter.
    /// </summary>
    [Required]
    [EnumDataType(typeof(FlightType))]
    public FlightType FlightType { get; set; }

    /// <summary>
    /// Flight price will be calculated automatically based on the base price.
    /// </summary>
    [Required]
    public decimal BasePriceNIS { get; set; }
  }
}