using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Persistence.Contracts;
using Domain;
using MediatR;

namespace Application.Features.Bets.Handlers.Commands;

public class CreateBetRequestHandler : IRequestHandler<CreateBetCommand, BetId>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public CreateBetRequestHandler(IBetMapper betMapper, IBetRepository betRepository)
    {
        _betMapper = betMapper;
        _betRepository = betRepository;
    }

    public async Task<BetId> Handle(CreateBetCommand request, CancellationToken cancellationToken)
    {
        var bet = _betMapper.DtoToBet(request.BetDto);
        var created = await _betRepository.AddAsync(bet);
        return created.Id;
    }
}
