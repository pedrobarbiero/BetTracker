using Domain.Enums;

namespace Application.Dtos.Bankroll;

public record GetBankrollDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Currency CurrentBalance { get; set; }
}
