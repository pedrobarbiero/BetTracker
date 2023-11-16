using Application.Dtos.Bet;
using Domain;
using MediatR;

namespace Application.Features.Bets.Requests.Commands;

public record CreateBetCommand : IRequest<BetId>
{
    public required CreateBetDto BetDto { get; set; }
}
