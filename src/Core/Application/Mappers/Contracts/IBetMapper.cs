﻿using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Domain.Models;

namespace Application.Mappers.Contracts;

public interface IBetMapper
{
    public GetBetDetailDto BetToDto(Bet bet);
    public Bet DtoToBet(GetBetDetailDto betDto);
    public Bet ToBet(CreateBetCommand createBetCommand);
}
