using Core;
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

    public string[] Comments => FlightType switch
    {
      FlightType.Regular => new string[2] { CommentsConstants.SeatsIncluded, CommentsConstants.ApprovedFlight },
      FlightType.LowCost => new string[3] { CommentsConstants.FlightDoesNotIncludeLuggage, CommentsConstants.FlightDoesNotIncludeMeals, CommentsConstants.FullCancellationFee },
      FlightType.Charter => new string[2] { CommentsConstants.DepartureTimeMayVary, CommentsConstants.SeatSelectionOnlyAtCheckIn },
      _ => Array.Empty<string>(),
    };
  }
}
