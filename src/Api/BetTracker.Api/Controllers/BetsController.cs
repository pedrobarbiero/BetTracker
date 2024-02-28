using Application.Features.Bets.Requests.Commands;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateBet(CreateBetCommand createBetCommand)
    {
        var response = await _mediator.Send(createBetCommand);
        return Ok(response);
    }

}
