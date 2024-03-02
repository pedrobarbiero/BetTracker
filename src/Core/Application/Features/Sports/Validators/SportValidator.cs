using Application.Features.Sports.Requests.Commands;
using FluentValidation;

namespace Application.Features.Sports.Validators;

public class SportValidator : AbstractValidator<BaseSportCommand>
{
    public SportValidator()
    {
        RuleFor(t => t.Name)
            .Length(3, 50);
    }
}
