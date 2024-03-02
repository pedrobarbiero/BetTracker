using Application.Features.Sports.Requests.Commands;
using FluentValidation;

namespace Application.Features.Sports.Validators;

public class CreateSportCommandValidator : AbstractValidator<CreateSportCommand>
{
    public CreateSportCommandValidator()
    {
        RuleFor(t => t).SetValidator(new SportValidator());
    }
}
