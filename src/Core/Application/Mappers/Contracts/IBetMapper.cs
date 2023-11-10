using Application.Dtos;
using Domain;

namespace Application.Mappers.Contracts;

public interface IBetMapper
{
    public BetDto BetToDto(Bet bet);
    public Bet DtoToBet(BetDto betDto);
}
