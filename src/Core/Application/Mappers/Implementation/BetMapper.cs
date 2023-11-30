using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.Implementation;

[Mapper]
public partial class BetMapper : IBetMapper
{
    public partial GetBetDetailDto BetToDto(Bet bet);

    public partial Bet DtoToBet(GetBetDetailDto betDto);

    public partial Bet DtoToBet(CreateBetCommand createBetCommand);
}
