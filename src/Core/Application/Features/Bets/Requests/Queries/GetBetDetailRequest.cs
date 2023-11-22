using Application.Dtos;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetDetailRequest : IRequest<GetBetDto?>
{
    public required Guid BetId { get; set; }
}
