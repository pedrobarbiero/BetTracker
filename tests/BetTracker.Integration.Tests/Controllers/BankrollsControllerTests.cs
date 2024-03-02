
using Application.Common;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Responses;
using BetTracker.Integration.Tests.Factories;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace BetTracker.Integration.Tests.Controllers;

public class BankrollsControllerTests : BaseIntegrationTest<BankrollTestFactory>
{
    const string BANKROLLS_URL = "/api/Bankrolls";
    public BankrollsControllerTests(BankrollTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetBankrolls_WithValidRequestWithPagination_ReturnsSuccess()
    {
        // Act
        var queryParams = new Dictionary<string, string?>
        {
            { "PageNumber", "1" },
            { "PageSize", "2" }
        };
        var response = await authorizedClient.GetAsync(QueryHelpers.AddQueryString(BANKROLLS_URL, queryParams));
        var data = await response.Content.ReadFromJsonAsync<PagedResult<GetBankrollDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(2, data.Items.Count());
        Assert.Equal((uint)2, data.PageSize);
        Assert.True(data.HasNextPage);
    }

    [Fact]
    public async Task GetBankrolls_ShouldReturnOnlyUserBankrolls()
    {
        // Act
        var response = await authorizedClient.GetAsync(BANKROLLS_URL);
        var data = await response.Content.ReadFromJsonAsync<PagedResult<GetBankrollDto>>();

        var expectedBankrolls = await dbContext.Bankrolls.AsNoTracking().Where(t => t.ApplicationUserId == authorizedUser.Id).ToListAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(expectedBankrolls.Count, data.Items.Count());
        Assert.All(expectedBankrolls, bankroll => Assert.Contains(data.Items, dto => dto.Id == bankroll.Id));
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
            Currency = Currency.GBP,
            InitialBalance = 0.0m,
            StandardUnit = 1.0m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Act
        var response = await authorizedClient.PostAsJsonAsync(BANKROLLS_URL, createBankrollCommand);
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
        var bankroll = await dbContext.Bankrolls.AsNoTracking().FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        // Act
        var response = await authorizedClient.GetAsync($"{BANKROLLS_URL}/{bankroll.Id}");
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
        var response = await authorizedClient.GetAsync($"{BANKROLLS_URL}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateBankroll_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var createBankrollCommand = new CreateBankrollCommand
        {
            Name = "a",
            Currency = Currency.EUR,
            InitialBalance = -0.1m,
            StandardUnit = -0.1m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };

        // Act
        var response = await authorizedClient.PostAsJsonAsync(BANKROLLS_URL, createBankrollCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(data);
        Assert.False(data.Success);
        Assert.NotEmpty(data.Errors);
        Assert.Contains("Name", data.Errors.Keys);
        Assert.Contains("InitialBalance", data.Errors.Keys);
        Assert.Contains("StandardUnit", data.Errors.Keys);
        Assert.Contains("StartedAt", data.Errors.Keys);
    }

    [Fact]
    public async Task UpdateBankroll_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var bankroll = await dbContext.Bankrolls.AsNoTracking().FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        var updateBankrollCommand = new UpdateBankrollCommand
        {
            Id = bankroll.Id,
            Name = "Updated Bankroll",
            Currency = Currency.GBP,
            InitialBalance = 0.0m,
            StandardUnit = 1.0m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{BANKROLLS_URL}/{bankroll.Id}", updateBankrollCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.True(data.Success);
        Assert.Empty(data.Errors);
        Assert.Equal(bankroll.Id, data.Id);
        Assert.True(data.Success);
        Assert.NotNull(data.Message);
        Assert.NotEmpty(data.Message);
    }

    [Fact]
    public async Task UpdateBankroll_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var bankroll = await dbContext.Bankrolls.AsNoTracking().FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        var updateBankrollCommand = new UpdateBankrollCommand
        {
            Id = bankroll.Id,
            Name = "a",
            Currency = Currency.EUR,
            InitialBalance = -0.1m,
            StandardUnit = -0.1m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{BANKROLLS_URL}/{bankroll.Id}", updateBankrollCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(data);
        Assert.False(data.Success);
        Assert.NotEmpty(data.Errors);
        Assert.Contains("Name", data.Errors.Keys);
        Assert.Contains("InitialBalance", data.Errors.Keys);
        Assert.Contains("StandardUnit", data.Errors.Keys);
        Assert.Contains("StartedAt", data.Errors.Keys);
    }

    [Fact]
    public async Task UpdateBankroll_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateBankrollCommand = new UpdateBankrollCommand
        {
            Id = id,
            Name = "Updated Bankroll",
            Currency = Currency.GBP,
            InitialBalance = 0.0m,
            StandardUnit = 1.0m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{BANKROLLS_URL}/{id}", updateBankrollCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Id", data.Errors.Keys);
    }

    [Fact]
    public async Task UpdateBankroll_WithDifferentIdInRequest_ReturnsBadRequest()
    {
        // Arrange
        var bankroll = await dbContext.Bankrolls.AsNoTracking().FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        var updateBankrollCommand = new UpdateBankrollCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Bankroll",
            Currency = Currency.GBP,
            InitialBalance = 0.0m,
            StandardUnit = 1.0m,
            StartedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{BANKROLLS_URL}/{bankroll.Id}", updateBankrollCommand);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}
