using Application.Dtos.Bet;
using Application.Features.Bets.Requests.Commands;
using FluentValidation;

namespace Application.Features.Bets.Validators;

public class CreateBetCommandValidator : AbstractValidator<CreateBetCommand>
{
    public CreateBetCommandValidator()
    {
        RuleFor(t => t.BetDto.TotalStake).GreaterThan(1000);
        // Todo: create all the other rules
    }
}
