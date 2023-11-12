using Application.Dtos;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetListRequest : IRequest<IEnumerable<GetBetDto>>
{
    public uint Page { get; set; }
    public uint PageSize { get; set; }
}
