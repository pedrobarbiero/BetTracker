using Application.Dtos;
using Domain;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetDetailRequest : IRequest<GetBetDto?>
{
    public required BetId BetId { get; set; }
}
