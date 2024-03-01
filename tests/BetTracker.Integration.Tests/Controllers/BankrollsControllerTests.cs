
using Application.Common;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
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
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 1" },
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 2" },
            new () { Id = Guid.NewGuid(), Name = "Test Bankroll 3" },
        };
        await betTrackerDbContext.Bankrolls.AddRangeAsync(bankrolls);
        await betTrackerDbContext.SaveChangesAsync();   

        // Act
        var queryParams = new Dictionary<string, string?>
        {
            { "PageNumber", "1" },
            { "PageSize", "2" }
        };
        var response = await authorizedClient.GetAsync(QueryHelpers.AddQueryString("/api/Bankrolls", queryParams));
        var data = await response.Content.ReadFromJsonAsync<PagedResult<GetBankrollDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(2, data.Items.Count());
        Assert.Equal((uint)2, data.PageSize);
        Assert.True(data.HasNextPage);
    }

    [Fact]
    public async Task CreateBankroll_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var createBankrollCommand = new CreateBankrollCommand
        {
            Id = id,
            Name = "Main Bankroll",
            CurrentBalance = 0.0m,
            InitialBalance = 0.0m,
            StandardUnit = 1.0m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Act
        var response = await authorizedClient.PostAsJsonAsync("/api/Bankrolls", createBankrollCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.EndsWith(id.ToString(), response.Headers.Location?.ToString());
        Assert.NotNull(data);
        Assert.True(data.Success);
        Assert.Empty(data.Errors);
        Assert.Equal(id, data.Id);
        Assert.True(data.Success);
        Assert.NotNull(data.Message);
        Assert.NotEmpty(data.Message);
    }

    [Fact]
    public async Task GetBankroll_WithValidId_ReturnsSuccess()
    {
        // Arrange
        var bankroll = new Bankroll { Id = Guid.NewGuid(), Name = "Test Bankroll" };
        await betTrackerDbContext.Bankrolls.AddAsync(bankroll);
        await betTrackerDbContext.SaveChangesAsync();

        // Act
        var response = await authorizedClient.GetAsync($"/api/bankrolls/{bankroll.Id}");
        var data = await response.Content.ReadFromJsonAsync<GetBankrollDto>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(bankroll.Id, data.Id);
        Assert.Equal(bankroll.Name, data.Name);
    }

    [Fact]
    public async Task GetBankroll_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await authorizedClient.GetAsync($"/api/Bankrolls/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
