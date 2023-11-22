using Domain;
using MediatR;

namespace Application.Features.Bets.Requests.Commands;

public record DeleteBetCommand : IRequest<bool>
{
    public required Guid Id { get; set; }
}
