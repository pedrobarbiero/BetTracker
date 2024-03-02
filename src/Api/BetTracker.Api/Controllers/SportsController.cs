using Application.Common;
using Application.Dtos.Sport;
using Application.Features.Sports.Requests.Queries;
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
    public async Task<ActionResult<PagedResult<GetSportDto>>> GetSports([FromQuery] GetSportsListQuery query)
    {
        var bankrolls = await _mediator.Send(query);
        return Ok(bankrolls);
    }
}
