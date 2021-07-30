﻿using Core.POCO;
using System;

namespace Infrastructure.Models
{
  public class Filters
  {
    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter
    /// </summary>
    public FlightType? FlightType { get; set; }

    /// <summary>
    /// Search Flight starting from this date
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Search Flight before this date
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Search Flight with price starting from this PriceFromInNIS
    /// </summary>
    public decimal? PriceFromInNIS { get; set; }

    /// <summary>
    /// Search Flight with price up to PriceToInNIS
    /// </summary>
    public decimal? PriceToInNIS { get; set; }

    /// <summary>
    /// Search Flight with Arrival Airoport Code
    /// </summary>
    public string ToAirportIATACode { get; set; }

    /// <summary>
    /// Search Flight with Departure Airoport Code
    /// </summary>
    public string FromAirportIATACode { get; set; }

    /// <summary>
    /// Store info for pagination
    /// </summary>
    public PagingInfo PagingInfo { get; set; }
  }
}