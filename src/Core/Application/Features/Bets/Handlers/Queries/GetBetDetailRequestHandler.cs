﻿using Application.Dtos;
using Application.Features.Bets.Requests.Queries;
using Application.Mappers.Contracts;
using Application.Persistence.Contracts;
using MediatR;

namespace Application.Features.Bets.Handlers.Queries;

public class GetBetDetailRequestHandler : IRequestHandler<GetBetDetailRequest, GetBetDto>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;
    public GetBetDetailRequestHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
        _betMapper = betMapper;
    }
    public async Task<GetBetDto?> Handle(GetBetDetailRequest request, CancellationToken cancellationToken)
    {
        var bet = await _betRepository.GetByIdAsync(request.BetId);
        if (bet == null)
            return null;

        return _betMapper.BetToDto(bet);
    }
}