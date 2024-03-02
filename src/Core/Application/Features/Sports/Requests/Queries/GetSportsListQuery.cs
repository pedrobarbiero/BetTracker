using Application.Common;
using Application.Dtos.Sport;
using MediatR;

namespace Application.Features.Sports.Requests.Queries;

public record GetSportsListQuery : PagedQuery<GetSportDto>, IRequest<PagedResult<GetSportDto>>
{
}
