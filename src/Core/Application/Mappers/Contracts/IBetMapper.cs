using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Domain;

namespace Application.Mappers.Contracts;

public interface IBetMapper
{
    public GetBetDetailDto BetToDto(Bet bet);
    public Bet DtoToBet(GetBetDetailDto betDto);
    public Bet DtoToBet(CreateBetCommand createBetCommand);
}
