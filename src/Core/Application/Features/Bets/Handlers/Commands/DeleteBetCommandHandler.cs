using Application.Features.Bets.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bets.Handlers.Commands;

public record DeleteBetCommandHandler : IRequestHandler<DeleteBetCommand, bool>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public DeleteBetCommandHandler(IBetRepository betRepository, IBetMapper betMapper)
    {
        _betRepository = betRepository;
        _betMapper = betMapper;
    }

    public Task<bool> Handle(DeleteBetCommand request, CancellationToken cancellationToken)
    {
        return _betRepository.DeleteAsync(request.Id);
    }
}
