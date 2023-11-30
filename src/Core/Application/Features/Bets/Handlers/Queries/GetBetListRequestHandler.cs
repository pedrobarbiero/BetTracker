﻿using Application.Contracts.Persistence;
using Application.Dtos;
using Application.Features.Bets.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Bets.Handlers.Queries;

public class GetBetListRequestHandler : IRequestHandler<GetBetListQuery, IEnumerable<GetBetDetailDto>>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public GetBetListRequestHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
        _betMapper = betMapper;
    }

    public async Task<IEnumerable<GetBetDetailDto>> Handle(GetBetListQuery request, CancellationToken cancellationToken)
    {
        var bets = await _betRepository.GetPagedAsync(request.Page, request.PageSize);
        return bets.Select(_betMapper.BetToDto);
    }
}
