using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Commands;
using Domain.Models;

namespace Application.Mappers.Contracts;

public interface ISportMapper
{
    public Sport ToSport(CreateSportCommand command);
    public Sport ToSport(UpdateSportCommand command);
    public GetSportDto GetSportDto(Sport sport);
}
