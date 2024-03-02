using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Sports.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using MediatR;

namespace Application.Features.Sports.Handlers.Commands;

public class UpdateSportCommandHandler : IRequestHandler<UpdateSportCommand, BaseCommandResponse>
{
    private readonly ISportMapper _sportMapper;
    private readonly ISportRepository _sportRepository;
    private readonly IUserProvider _userProvider;
    public UpdateSportCommandHandler(ISportMapper sportMapper, ISportRepository sportRepository, IUserProvider userProvider)
    {
        _sportMapper = sportMapper;
        _sportRepository = sportRepository;
        _userProvider = userProvider;
    }

    public Task<BaseCommandResponse> Handle(UpdateSportCommand command, CancellationToken cancellationToken)
    {
        command.ApplicationUserId = _userProvider.GetCurrentUserId();
        var sport = _sportMapper.ToSport(command);
        _sportRepository.Update(sport);
        return Task.FromResult(new BaseCommandResponse()
        {
            Id = command.Id,
            Success = true,
            Message = "Sport updated successfully"
        });
    }
}
