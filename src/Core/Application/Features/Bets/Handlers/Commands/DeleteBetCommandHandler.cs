using Application.Contracts.Persistence;
using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Bets.Handlers.Commands;

public record DeleteBetCommandHandler : IRequestHandler<DeleteBetCommand, bool>
{
    private readonly IBetRepository _betRepository;

    public DeleteBetCommandHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
    }

    public Task<bool> Handle(DeleteBetCommand request, CancellationToken cancellationToken)
    {
        return _betRepository.DeleteAsync(request.Id);
    }
}
