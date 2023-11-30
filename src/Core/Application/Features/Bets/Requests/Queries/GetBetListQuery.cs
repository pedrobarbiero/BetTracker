using Application.Dtos;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetListQuery : IRequest<IEnumerable<GetBetDetailDto>>
{
    public uint Page { get; set; }
    public uint PageSize { get; set; }
}
