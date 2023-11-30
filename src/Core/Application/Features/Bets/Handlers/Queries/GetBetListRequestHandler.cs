using Application.Common;
using Application.Contracts.Persistence;
using Application.Dtos;
using Application.Features.Bets.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Bets.Handlers.Queries;

public class GetBetListRequestHandler : IRequestHandler<GetBetListQuery, PagedResult<GetBetDetailDto>>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public GetBetListRequestHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
        _betMapper = betMapper;
    }

    public async Task<PagedResult<GetBetDetailDto>> Handle(GetBetListQuery request, CancellationToken cancellationToken)
    {
        var paged = await _betRepository.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<GetBetDetailDto>()
        {
            Items = paged.Items.Select(_betMapper.BetToDto),
            HasNextPage = paged.HasNextPage,
            Page = paged.Page,
            PageSize = paged.PageSize
        };
    }
}
