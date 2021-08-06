using ClientMVC.Models;
using FluentValidation;
using System;

namespace ClientMVC.Validators
{
  public class AddFlightRequestValidatorcs : AbstractValidator<AddFlightViewModel>
  {
    public AddFlightRequestValidatorcs()
    {
      RuleFor(request => request.DepartureAirportIATA)
               .NotEmpty()
               .WithMessage("{PropertyName} is required")
               .MinimumLength(3)
               .MaximumLength(3)
               .WithMessage("Departure Airport Code Must have lenght 3 character")
               .NotEqual(request => request.ArrivalAirportIATA)
               .WithMessage("Departure Airport Code Must Be Not Equal To Arrival Airport");

      RuleFor(request => request.ArrivalAirportIATA)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(3)
            .MaximumLength(3)
            .WithMessage("Arival Airport Code Must have lenght 3 character");

      RuleFor(request => request.DepartureDateTime)
             .NotEmpty()
             .WithMessage("{PropertyName} is required")
             .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
             .WithMessage("{PropertyName} must be today or grater");

      RuleFor(request => request.ArrivalDateTime)
       .NotEmpty()
       .WithMessage("{PropertyName} is required")
       .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
       .WithMessage("{PropertyName} must be today or grater");
    }
  }
}