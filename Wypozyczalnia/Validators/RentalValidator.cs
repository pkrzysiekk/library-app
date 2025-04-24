using FluentValidation;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Validators;

public class RentalValidator : AbstractValidator<RentalViewModel>
{
    public RentalValidator()
    {
        RuleFor(r => r.ClientName)
            .NotEmpty()
            .WithMessage("Client name is required.")
            .Length(2, 50)
            .WithMessage("Client name must be between 2 and 50 characters.");
        RuleFor(r => r.ClientLastName)
            .NotEmpty()
            .WithMessage("Client last name is required.")
            .Length(2, 50)
            .WithMessage("Client last name must be between 2 and 50 characters.");
        RuleFor(r => r.ExpectedReturnDate)
            .NotEmpty() 
            .WithMessage("Return date is required.")
            .GreaterThanOrEqualTo(r => r.RentalDate)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Return date must be after rental date.");
        RuleFor(r => r.ActualReturnDate)
            .GreaterThanOrEqualTo(r => r.RentalDate)
            .When(r => r.ActualReturnDate.HasValue)
            .WithMessage("Actual return date must be before or equal to the expected return date.");
        RuleFor(r => r.Charge)
            .GreaterThanOrEqualTo(0)
            .When(r => r.Charge.HasValue)
            .WithMessage("Charge must be a positive value.");
    }
}