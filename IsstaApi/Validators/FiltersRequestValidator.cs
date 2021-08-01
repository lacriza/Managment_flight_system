using FluentValidation;
using System;
using Web.Requests;

namespace Web.Validators
{
  public class FiltersRequestValidator : AbstractValidator<FiltersRequest>
  {
    public FiltersRequestValidator()
    {
      When(request => request!= null, () =>
      {
        RuleFor(request => request.PagingInfo)
        .NotEmpty().WithMessage("{PropertyName} is required");

        When(request => request.FlightType != null, () =>
        {
          RuleFor(request => request.FlightType)
              .IsInEnum()
              .NotEmpty()
              .WithMessage("{PropertyName} is required");
        });

        When(request => request.DateFrom != null, () =>
        {
          RuleFor(request => request.DateFrom)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
                .WithMessage("{PropertyName} must be today or grater");
        });

        When(request => request.DateTo != null, () =>
        {
          RuleFor(request => request.DateTo)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThanOrEqualTo(r => DateTime.UtcNow.Date.AddDays(-1))
                .WithMessage("{PropertyName} must be today or grater");
        });

        When(request => request.PriceFromInNIS != null && request.PriceToInNIS != null, () =>
        {
          RuleFor(request => request.PriceToInNIS)
             .GreaterThanOrEqualTo(r => r.PriceFromInNIS)
             .WithMessage("Up-Price must be greater or equal to From-Price");
        });

        When(request => request.FromAirportIATACode != null, () =>
        {
          RuleFor(request => request.FromAirportIATACode)
                          .NotEmpty()
                          .MinimumLength(3)
                          .MaximumLength(3)
          .WithMessage("Departure Airport Code Must have lenght 3 character");
        });

        When(request => request.ToAirportIATACode != null, () =>
        {
          RuleFor(request => request.ToAirportIATACode)
                          .NotEmpty()
                          .MinimumLength(3)
                          .MaximumLength(3)
          .WithMessage("Arival Airport Code Shoud Be 3 character lenght");
        });

      });
    }

   
  }
}
