using Application.Features.Bankrolls.Requests.Commands;
using FluentValidation;

namespace Application.Features.Bankrolls.Validators;

public class BankrollValidator : AbstractValidator<BaseBankrollCommand>
{
    public BankrollValidator()
    {
        RuleFor(t => t.Name)
            .Length(3, 50);

        RuleFor(t => t.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .LessThan(1_000_000);

        RuleFor(t => t.Currency)
            .IsInEnum();

        RuleFor(t => t.StartedAt)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(t => t.StandardUnit)
            .GreaterThan(0)
            .LessThan(1_000_000);
    }
}
