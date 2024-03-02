using Application.Responses;
using MediatR;

namespace Application.Common;

public record DeleteByIdCommand : IRequest<BaseCommandResponse>
{
    public Guid Id { get; set; }
}
