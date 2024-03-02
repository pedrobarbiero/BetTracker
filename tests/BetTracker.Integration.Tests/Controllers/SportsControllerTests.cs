using Application.Common;
using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Commands;
using Application.Responses;
using BetTracker.Integration.Tests.Factories;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace BetTracker.Integration.Tests.Controllers;

public class SportsControllerTests : BaseIntegrationTest<SportTestWebAppFactory>
{
    const string SPORTS_URL = "/api/sports";

    public SportsControllerTests(SportTestWebAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task GetSports_WithValidRequestWithPagination_ReturnsSuccess()
    {
        // Arrange
        var count = dbContext.Sports
            .Where(t => t.ApplicationUserId == authorizedUser.Id || t.ApplicationUserId == Domain.Constants.Users.JokerId)
            .Count();
        var pageSize = (count - 1).ToString();

        // Act
        var queryParams1 = new Dictionary<string, string?>
        {
            { "Page", "1" },
            { "PageSize", pageSize }
        };
        var response1 = await authorizedClient.GetAsync(QueryHelpers.AddQueryString(SPORTS_URL, queryParams1));
        var data1 = await response1.Content.ReadFromJsonAsync<PagedResult<GetSportDto>>();

        var queryParams2 = new Dictionary<string, string?>
        {
            { "Page", "2" },
            { "PageSize", pageSize }
        };
        var response2 = await authorizedClient.GetAsync(QueryHelpers.AddQueryString(SPORTS_URL, queryParams2));
        var data2 = await response2.Content.ReadFromJsonAsync<PagedResult<GetSportDto>>();

        // Assert
        Assert.True(response1.IsSuccessStatusCode);
        Assert.NotNull(data1);
        Assert.Equal(count - 1, data1.Items.Count());
        Assert.True(data1.HasNextPage);

        Assert.True(response2.IsSuccessStatusCode);
        Assert.NotNull(data2);
        Assert.Equal(1, data2.Items.Count());
        Assert.False(data2.HasNextPage);
    }

    [Fact]
    public async Task GetSports_ShouldReturnOnlyUserSports()
    {
        // Arrange
        var expectedSports = await dbContext.Sports.AsNoTracking()
            .Where(t => t.ApplicationUserId == authorizedUser.Id || t.ApplicationUserId == Domain.Constants.Users.JokerId)
            .ToListAsync();

        // Act
        var queryParams = new Dictionary<string, string?>
        {
            { "Page", "1" },
            { "PageSize", "100" }
        };
        var response = await authorizedClient.GetAsync(QueryHelpers.AddQueryString(SPORTS_URL, queryParams));
        var data = await response.Content.ReadFromJsonAsync<PagedResult<GetSportDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(expectedSports.Count, data.Items.Count());
        Assert.All(expectedSports, sport => Assert.Contains(data.Items, dto => dto.Id == sport.Id));
    }

    [Fact]
    public async Task CreateSport_WithValidRequest_ReturnSuccess()
    {
        // Arrange
        var id = Guid.NewGuid();
        var createSportCommand = new CreateSportCommand
        {
            Id = id,
            Name = "E-sports",
            Slug = "e-sports"
        };

        // Act
        var response = await authorizedClient.PostAsJsonAsync(SPORTS_URL, createSportCommand);
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
    public async Task CreateSport_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var createSportCommand = new CreateSportCommand
        {
            Id = Guid.NewGuid(),
            Name = "",
            Slug = ""
        };

        // Act
        var response = await authorizedClient.PostAsJsonAsync(SPORTS_URL, createSportCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(data);
        Assert.False(data.Success);
        Assert.NotEmpty(data.Errors);
        Assert.Contains("Name", data.Errors.Keys);
    }

    [Fact]
    public async Task GetSport_WithValidId_ReturnsSuccess()
    {
        // Arrange
        var sport = await dbContext.Sports.AsNoTracking()
            .FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        // Act
        var response = await authorizedClient.GetAsync($"{SPORTS_URL}/{sport.Id}");
        var data = await response.Content.ReadFromJsonAsync<GetSportDto>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(sport.Id, data.Id);
        Assert.Equal(sport.Name, data.Name);
    }

    [Fact]
    public async Task GetSport_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await authorizedClient.GetAsync($"{SPORTS_URL}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSport_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var sport = await dbContext.Sports.AsNoTracking()
            .FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);
        var updateSportCommand = new UpdateSportCommand
        {
            Id = sport.Id,
            Name = "E-sports",
            Slug = "e-sports"
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{SPORTS_URL}/{sport.Id}", updateSportCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.Equal(sport.Id, data.Id);
        Assert.True(data.Success);
        Assert.NotNull(data.Message);
        Assert.NotEmpty(data.Message);
    }

    [Fact]
    public async Task UpdateSport_WithDifferentIdInRequest_ReturnsBadRequest()
    {
        var sport = await dbContext.Sports.AsNoTracking()
            .FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        var updateSportCommand = new UpdateSportCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Sport",
            Slug = "updated-sport"
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{SPORTS_URL}/{sport.Id}", updateSportCommand);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSport_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateSportCommand = new UpdateSportCommand
        {
            Id = id,
            Name = "Updated Sport",
            Slug= "updated-sport"
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{SPORTS_URL}/{id}", updateSportCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Id", data.Errors.Keys);
    }

    [Fact]
    public async Task UpdateSport_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var sport = await dbContext.Sports
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ApplicationUserId == authorizedUser.Id);

        var updateSportCommand = new UpdateSportCommand
        {
            Id = sport.Id,
            Name = "a",
            Slug = ""
        };

        // Act
        var response = await authorizedClient.PutAsJsonAsync($"{SPORTS_URL}/{sport.Id}", updateSportCommand);
        var data = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(data);
        Assert.False(data.Success);
        Assert.NotEmpty(data.Errors);
        Assert.Contains("Name", data.Errors.Keys);
    }
}
