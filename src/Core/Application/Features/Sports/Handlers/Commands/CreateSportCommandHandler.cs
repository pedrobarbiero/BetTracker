using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Sports.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using MediatR;

namespace Application.Features.Sports.Handlers.Commands;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, BaseCommandResponse>
{
    private readonly ISportMapper _sportMapper;
    private readonly ISportRepository _sportRepository;
    private readonly IIdProvider _idProvider;
    private readonly IUserProvider _userProvider;

    public CreateSportCommandHandler(ISportMapper sportMapper, ISportRepository sportRepository, IIdProvider idProvider, IUserProvider userProvider)
    {
        _sportMapper = sportMapper;
        _sportRepository = sportRepository;
        _idProvider = idProvider;
        _userProvider = userProvider;
    }

    public async Task<BaseCommandResponse> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        request.Id = _idProvider.GetNextIdIfEmptyOrNull(request.Id);
        request.ApplicationUserId = _userProvider.GetCurrentUserId();
        var sport = _sportMapper.ToSport(request);
        var created = await _sportRepository.AddAsync(sport);

        return new BaseCommandResponse()
        {
            Id = created.Id,
            Success = true,
            Message = "Sport created successfully"
        };
    }
}
