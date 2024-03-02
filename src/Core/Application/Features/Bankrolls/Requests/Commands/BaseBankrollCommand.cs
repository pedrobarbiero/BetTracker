using Application.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Bankrolls.Requests.Commands;

public class BaseBankrollCommand : IRequest<BaseCommandResponse>
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public decimal InitialBalance { get; set; }
    public Currency Currency { get; set; }
    public DateOnly StartedAt { get; set; }
    public decimal StandardUnit { get; set; }
    public Guid? ApplicationUserId { get; set; }
}
