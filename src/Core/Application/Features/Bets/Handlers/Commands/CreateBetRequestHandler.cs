using Application.Contracts.Persistence;
using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using MediatR;

namespace Application.Features.Bets.Handlers.Commands;

public class CreateBetRequestHandler : IRequestHandler<CreateBetCommand, BaseCommandResponse<Guid>>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public CreateBetRequestHandler(IBetMapper betMapper, IBetRepository betRepository)
    {
        _betMapper = betMapper;
        _betRepository = betRepository;
    }

    public async Task<BaseCommandResponse<Guid>> Handle(CreateBetCommand request, CancellationToken cancellationToken)
    {
        var bet = _betMapper.DtoToBet(request.BetDto);
        var created = await _betRepository.AddAsync(bet);
        return new BaseCommandResponse<Guid>()
        {
            Data = created.Id,
            Success = true,
            Message = "Bet created successfully"
        };
    }
}
