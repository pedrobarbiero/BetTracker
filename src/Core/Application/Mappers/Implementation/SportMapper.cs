using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Commands;
using Application.Mappers.Contracts;
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.Implementation;

[Mapper]
public partial class SportMapper : ISportMapper
{
    public partial GetSportDto GetSportDto(Sport sport);
    public partial Sport ToSport(UpdateSportCommand command);
    public partial Sport ToSport(CreateSportCommand command);
}
