using Application.Dtos.Bet;
using Application.Responses;
using MediatR;

namespace Application.Features.Bets.Requests.Commands;

public record CreateBetCommand : IRequest<BaseCommandResponse>
{
    public required CreateBetDto BetDto { get; set; }
}
