using Application.Common;
using Application.Contracts.Persistence;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Queries;

public class GetBankrollListQueryHandler : IRequestHandler<GetBankrollListQuery, PagedResult<GetBankrollDto>>
{
    private readonly IBankrollRepository _bankrollRepository;
    private readonly IBankrollMapper _bankrollMapper;

    public GetBankrollListQueryHandler(IBankrollMapper bankrollMapper, IBankrollRepository bankrollRepository)
    {
        _bankrollMapper = bankrollMapper;
        _bankrollRepository = bankrollRepository;
    }

    public async Task<PagedResult<GetBankrollDto>> Handle(GetBankrollListQuery request, CancellationToken cancellationToken)
    {
        var paged = await _bankrollRepository.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<GetBankrollDto>()
        {
            Items = paged.Items.Select(_bankrollMapper.GetBankrollDto),
            HasNextPage = paged.HasNextPage,
            Page = paged.Page,
            PageSize = paged.PageSize
        };
    }
}
