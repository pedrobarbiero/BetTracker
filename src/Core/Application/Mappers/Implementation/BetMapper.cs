using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.Implementation;

[Mapper]
public partial class BetMapper : IBetMapper
{
    public partial GetBetDto BetToDto(Bet bet);

    public partial Bet DtoToBet(GetBetDto betDto);

    public partial Bet DtoToBet(CreateBetCommand createBetCommand);
}
