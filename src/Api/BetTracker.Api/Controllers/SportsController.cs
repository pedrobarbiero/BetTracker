using Application.Common;
using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Commands;
using Application.Features.Sports.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SportsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<GetSportDto>>> GetSports([FromQuery] GetSportListQuery query)
    {
        var sports = await _mediator.Send(query);
        return Ok(sports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetSportDto>> GetSport(Guid id)
    {
        var sport = await _mediator.Send(new GetSportByIdQuery { Id = id });
        if (sport is null) return NotFound();
        return Ok(sport);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateSport(CreateSportCommand createSportCommand)
    {
        var response = await _mediator.Send(createSportCommand);
        if (!response.Success) return BadRequest(response);
        return CreatedAtAction(nameof(GetSport), new { id = createSportCommand.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseCommandResponse>> UpdateSport(Guid id, UpdateSportCommand updateSportCommand)
    {
        if (id != updateSportCommand.Id) return BadRequest();
        var response = await _mediator.Send(updateSportCommand);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteSport(Guid id)
    {
        var response = await _mediator.Send(new DeleteSportByIdCommand { Id = id });
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }
}
