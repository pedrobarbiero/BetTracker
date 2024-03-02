namespace Application.Dtos.Sport;

public record GetSportDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
}
