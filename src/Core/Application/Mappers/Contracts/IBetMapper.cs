using Application.Dtos;
using Application.Dtos.Bet;
using Domain;

namespace Application.Mappers.Contracts;

public interface IBetMapper
{
    public GetBetDto BetToDto(Bet bet);
    public Bet DtoToBet(GetBetDto betDto);
    public Bet DtoToBet(CreateBetDto betDto);
}
