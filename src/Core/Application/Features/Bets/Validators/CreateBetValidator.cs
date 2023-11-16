using Application.Dtos.Bet;
using FluentValidation;

namespace Application.Features.Bets.Validators;

public class CreateBetValidator : AbstractValidator<CreateBetDto>
{
    public CreateBetValidator()
    {
        RuleFor(bet => bet.Id).NotEmpty();
        RuleFor(bet => bet.BrankroolId).NotEmpty();
        // Todo: create all the other rules
    }
}
