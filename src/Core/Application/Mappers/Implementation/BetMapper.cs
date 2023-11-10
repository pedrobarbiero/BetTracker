using Application.Dtos;
using Application.Mappers.Contracts;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.Implementation;

[Mapper]
public partial class BetMapper : IBetMapper
{
    public partial BetDto BetToDto(Bet bet);

    public partial Bet DtoToBet(BetDto betDto);
}
