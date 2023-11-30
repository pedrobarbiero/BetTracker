using Application.Common;
using Application.Dtos.Bankroll;
using MediatR;

namespace Application.Features.Bankrolls.Requests.Queries;

public record GetBankrollListQuery : PagedQuery<GetBankrollDto>, IRequest<PagedResult<GetBankrollDto>>
{
}
