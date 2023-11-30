using Application.Common;
using Application.Dtos;
using MediatR;

namespace Application.Features.Bets.Requests.Queries;

public record GetBetListQuery : PagedQuery<GetBetDetailDto>, IRequest<IEnumerable<GetBetDetailDto>>
{
}
