﻿using Application.Common;
using Domain;
using Domain.Enums;

namespace Application.Dtos;

public record GetBetDetailDto : BaseDto
{
    //Todo: finish DTOs
    public required Guid BrankroolId { get; set; }
    public Guid? TipsterId { get; set; }
    public required Guid BettingMarketId { get; set; }
    public required BettingMarket BettingMarket { get; set; }
    public bool PreMatch { get; set; }
    public bool Settled { get; set; }
    public string? Description { get; set; }
    public decimal Odd { get; set; }
    public decimal TotalStake { get; set; }
    public decimal TotalReturn { get; set; }
    public decimal PotentialReturn { get; set; }
    public decimal ExpectedYield { get; set; }
    public bool IsMultiple { get; set; }
    public BetResult Result { get; set; }
}