using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Features.Bankrolls.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankrollController : ControllerBase
{
    private readonly IMediator _mediator;
    public BankrollController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetBankrollDto>>> GetBankrolls([FromQuery] GetBankrollListQuery query)
    {
        var bankrolls = await _mediator.Send(query);
        return Ok(bankrolls);
    }
    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateBankroll(CreateBankrollCommand createBankrollCommand)
    {
        var response = await _mediator.Send(createBankrollCommand);
        return Ok(response);
    }
}
