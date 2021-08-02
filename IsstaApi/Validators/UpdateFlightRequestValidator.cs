using FluentValidation;
using System;
using Web.Requests;

namespace Web.Validators
{
  public class UpdateFlightRequestValidator : AbstractValidator<UpdateFlightRequest>
  {
    public UpdateFlightRequestValidator()
    {
      RuleFor(request => request.FlightNumber)
           .NotEmpty()
           .WithMessage("{PropertyName} is required");

      When(request => request.DepartureDateTime != null, () =>
      {
        RuleFor(request => request.DepartureDateTime)
           .NotEmpty()
           .WithMessage("{PropertyName} is required")
           .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
           .WithMessage("{PropertyName} must be today or grater");
      });

      When(request => request.ArrivalDateTime != null, () =>
      {
        RuleFor(request => request.ArrivalDateTime)
       .NotEmpty()
       .WithMessage("{PropertyName} is required")
       .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
       .WithMessage("{PropertyName} must be today or grater");
      });

      When(request => request.BasePriceNIS != null, () =>
      {
        RuleFor(request => request.BasePriceNIS)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("{PropertyName} is required");
      });

      When(request => request.FlightType != null, () =>
      {
        RuleFor(request => request.FlightType)
               .IsInEnum()
               .WithMessage("FlightType must be valid enum value");
      });
    }
  }
}
