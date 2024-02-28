
using Application.Common;
using Application.Dtos.Bankroll;
using Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace BetTracker.Integration.Tests.Controllers;

public class BankrollsControllerTests : BaseIntegrationTest
{
    public BankrollsControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetBankrolls_WithValidRequestWithPagination_ReturnsSuccess()
    {
        // Arrange
        var bankrolls = new List<Bankroll>
        {
            new () { Id = Guid.NewGuid(),Name = "Test Bankroll 1" },
            new () { Id = Guid.NewGuid(),Name = "Test Bankroll 2" },
            new () { Id = Guid.NewGuid(),Name = "Test Bankroll 3" },
        };
        await betTrackerDbContext.Bankrolls.AddRangeAsync(bankrolls);
        await betTrackerDbContext.SaveChangesAsync();   

        // Act
        var queryParams = new Dictionary<string, string?>
        {
            { "PageNumber", "1" },
            { "PageSize", "2" }
        };
        var response = await client.GetAsync(QueryHelpers.AddQueryString("/api/Bankrolls", queryParams));
        var data = await response.Content.ReadFromJsonAsync<PagedResult<GetBankrollDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(2, data.Items.Count());
        Assert.Equal((uint)2, data.PageSize);
        Assert.True(data.HasNextPage);
    }
}
