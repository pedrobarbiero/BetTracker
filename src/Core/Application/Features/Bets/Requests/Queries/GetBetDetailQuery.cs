using Application.Dtos;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetDetailQuery : IRequest<GetBetDetailDto?>
{
    public required Guid BetId { get; set; }
}
