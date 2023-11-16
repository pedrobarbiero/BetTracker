using Application.Dtos.Bet;
using Application.Responses;
using Domain;
using MediatR;

namespace Application.Features.Bets.Requests.Commands;

public record CreateBetCommand : IRequest<BaseCommandResponse<BetId>>
{
    public required CreateBetDto BetDto { get; set; }
}
