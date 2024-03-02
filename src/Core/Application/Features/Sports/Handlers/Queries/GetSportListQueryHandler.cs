using Application.Common;
using Application.Contracts.Persistence;
using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Sports.Handlers.Queries;

public class GetSportListQueryHandler : IRequestHandler<GetSportsListQuery, PagedResult<GetSportDto>>
{
    private readonly ISportRepository _sportRepository;
    private readonly ISportMapper _sportMapper;

    public GetSportListQueryHandler(ISportMapper sportMapper, ISportRepository sportRepository)
    {
        _sportMapper = sportMapper;
        _sportRepository = sportRepository;
    }

    public async Task<PagedResult<GetSportDto>> Handle(GetSportsListQuery request, CancellationToken cancellationToken)
    {
        var paged = await _sportRepository.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<GetSportDto>()
        {
            Items = paged.Items.Select(_sportMapper.GetSportDto),
            HasNextPage = paged.HasNextPage,
            Page = paged.Page,
            PageSize = paged.PageSize
        };
    }
}
