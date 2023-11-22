using Application.Dtos;
using Application.Features.Bets.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class BetController : ControllerBase
{
    private readonly IMediator _mediator;

    public BetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetBetDto>>> GetBets()
    {
        var budgets = await _mediator.Send(new GetBetListRequest());
        return Ok(budgets);
    }

}
