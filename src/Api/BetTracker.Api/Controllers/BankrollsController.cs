using Application.Common;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Features.Bankrolls.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BankrollsController : ControllerBase
{
    private readonly IMediator _mediator;
    public BankrollsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<GetBankrollDto>>> GetBankrolls([FromQuery] GetBankrollListQuery query)
    {
        var bankrolls = await _mediator.Send(query);
        return Ok(bankrolls);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetBankrollDto>> GetBankroll(Guid id)
    {
        var bankroll = await _mediator.Send(new GetBankrollByIdQuery { Id = id });
        if (bankroll is null) return NotFound();
        return Ok(bankroll);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateBankroll(CreateBankrollCommand createBankrollCommand)
    {
        var response = await _mediator.Send(createBankrollCommand);
        if (!response.Success) return BadRequest(response);
        return CreatedAtAction(nameof(GetBankroll), new { id = createBankrollCommand.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseCommandResponse>> UpdateBankroll(Guid id, UpdateBankrollCommand updateBankrollCommand)
    {
        if (id != updateBankrollCommand.Id) return BadRequest();
        var response = await _mediator.Send(updateBankrollCommand);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteBankroll(Guid id)
    {
        var response = await _mediator.Send(new DeleteBankrollByIdCommand { Id = id });
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }
}
