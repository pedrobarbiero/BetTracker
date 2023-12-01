using Application.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Bankrolls.Requests.Commands;

public class CreateBankrollCommand : IRequest<BaseCommandResponse>
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public decimal InitialBalance { get; set; }
    public Currency CurrentBalance { get; set; }
    public DateTime StartedAt { get; set; }
    public decimal StandardUnit { get; set; }
}
