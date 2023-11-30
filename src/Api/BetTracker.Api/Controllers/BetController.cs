using Application.Dtos;
using Application.Features.Bets.Requests.Commands;
using Application.Features.Bets.Requests.Queries;
using Application.Responses;
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
    public async Task<ActionResult<IEnumerable<GetBetDetailDto>>> GetBets()
    {
        var budgets = await _mediator.Send(new GetBetListQuery());
        return Ok(budgets);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateBet(CreateBetCommand createBetCommand)
    {
        var response = await _mediator.Send(createBetCommand);
        return Ok(response);
    }

}
