namespace BetTracker.Integration.Tests.Controllers;

public class BankrollControllerTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    public BankrollControllerTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        _factory?.Dispose();
        _client?.Dispose();
    }

    [Fact]
    public async Task GetBankrolls_ReturnsSuccessStatusCode()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/bankroll");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
