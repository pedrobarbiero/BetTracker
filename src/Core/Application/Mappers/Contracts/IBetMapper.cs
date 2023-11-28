using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Domain;

namespace Application.Mappers.Contracts;

public interface IBetMapper
{
    public GetBetDto BetToDto(Bet bet);
    public Bet DtoToBet(GetBetDto betDto);
    public Bet DtoToBet(CreateBetCommand createBetCommand);
}
