using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientMVC.Models
{
  public class FlightViewModel 
    //: IValidatableObject
  {
    public string FlightNumber { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDateTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ArrivalDateTime { get; set; }

    public string ArrivalAirportIATA { get; set; }

    public string DepartureAirportIATA { get; set; }

    [Required]
    public FlightType FlightType { get; set; }

    public decimal TotalPriceNIS { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
    public decimal BasePriceNIS { get; set; }

    public string[] Comments { get; set; }

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //  if (DepartureDateTime > ArrivalDateTime)
    //  {
    //    yield return new ValidationResult(
    //        errorMessage: "ArrivalDateTime must be greater than DepartureDateTime",
    //        memberNames: new[] { "ArrivalDateTime" }
    //   );
    //  }
    //}
  }
}