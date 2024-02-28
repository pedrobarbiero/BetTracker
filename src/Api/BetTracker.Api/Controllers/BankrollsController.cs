using Application.Common;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Features.Bankrolls.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BetTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateBankroll(CreateBankrollCommand createBankrollCommand)
    {
        var response = await _mediator.Send(createBankrollCommand);
        return Ok(response);
    }
}
