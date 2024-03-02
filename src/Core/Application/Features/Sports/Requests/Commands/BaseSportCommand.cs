using Application.Responses;
using MediatR;

namespace Application.Features.Sports.Requests.Commands;

public class BaseSportCommand : IRequest<BaseCommandResponse>
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public Guid? ApplicationUserId { get; set; }
}
