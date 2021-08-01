using FluentValidation;
using System;
using Web.Requests;

namespace Web.Validators
{
  public class FlightRequestValidator : AbstractValidator<FlightRequest>
  {
    public FlightRequestValidator()
    {
      RuleFor(request => request.FlightType)
               .IsInEnum()
               .WithMessage("FlightType must be valid enum value");

      RuleFor(request => request.DepartureAirportIATA)
               .NotEmpty()
               .WithMessage("{PropertyName} is required")
               .MinimumLength(3)
               .MaximumLength(3)
               .WithMessage("Departure Airport Code Must have lenght 3 character");

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

      RuleFor(request => request.BasePriceNIS)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
    }
  }
}
