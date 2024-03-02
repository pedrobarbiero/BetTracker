using Application.Contracts.Persistence;
using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Sports.Handlers.Queries;

public class GetSportByIdQueryHandler : IRequestHandler<GetSportByIdQuery, GetSportDto?>
{
    private readonly ISportRepository _sportRepository;
    private readonly ISportMapper _sportMapper;

    public GetSportByIdQueryHandler(ISportMapper sportMapper, ISportRepository sportRepository)
    {
        _sportMapper = sportMapper;
        _sportRepository = sportRepository;
    }

    public async Task<GetSportDto?> Handle(GetSportByIdQuery request, CancellationToken cancellationToken)
    {
        var sport = await _sportRepository.GetByIdAsync(request.Id);

        if (sport is null) return null;

        return _sportMapper.GetSportDto(sport);
    }
}
