﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ClientMVC.Models
{
  /// <summary>
  /// All filters are optional
  /// </summary>
  public class FiltersRequest
  {
    /// <summary>
    /// Possible Flight type:  Regular, LowCost, Charter
    /// </summary>
    [EnumDataType(typeof(FlightType))]
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
    private PagingRequest _pagingInfo;
    public PagingRequest PagingInfo
    {
      get => (_pagingInfo ?? new PagingRequest());
      set => _pagingInfo = value;
    }
  }
}
