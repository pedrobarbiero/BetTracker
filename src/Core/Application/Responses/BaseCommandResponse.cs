namespace Application.Responses;

public class BaseCommandResponse<T>
{
    public T? Data { get; set; }
    public required bool Success { get; init; }
    public string? Message { get; init; }
    public Dictionary<string, string> Errors { get; init; } = new();
}
