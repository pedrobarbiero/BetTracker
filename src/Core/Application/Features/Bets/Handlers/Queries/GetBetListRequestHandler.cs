using Application.Dtos;
using Application.Features.Bets.Requests.Queries;
using Application.Mappers.Contracts;
using Application.Persistence.Contracts;
using MediatR;

namespace Application.Features.Bets.Handlers.Queries;

public class GetBetListRequestHandler : IRequestHandler<GetBetListRequest, IEnumerable<GetBetDto>>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public GetBetListRequestHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
        _betMapper = betMapper;
    }

    public async Task<IEnumerable<GetBetDto>> Handle(GetBetListRequest request, CancellationToken cancellationToken)
    {
        var bets = await _betRepository.GetPagedAsync(request.Page, request.PageSize);
        return bets.Select(_betMapper.BetToDto);
    }
}
