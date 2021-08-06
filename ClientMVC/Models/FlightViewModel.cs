using System;
using System.ComponentModel.DataAnnotations;

namespace ClientMVC.Models
{
  public class FlightViewModel
  {
    public string FlightNumber { get; set; }

    public DateTimeOffset DepartureDateTime { get; set; }

    public DateTimeOffset ArrivalDateTime { get; set; }

    public string ArrivalAirportIATA { get; set; }

    public string DepartureAirportIATA { get; set; }

    [Required]
    public FlightType FlightType { get; set; }

    public decimal TotalPriceNIS { get; set; }

    [Required]
    [Range(1.0, 100000)]
    public decimal BasePriceNIS { get; set; }

    public string[] Comments { get; set; }
  }
}