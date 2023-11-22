using Application.Features.Bets.Requests.Commands;
using Application.Features.Bets.Validators;
using Application.Mappers.Contracts;
using Application.Contracts.Persistence;
using Application.Responses;
using Domain;
using MediatR;

namespace Application.Features.Bets.Handlers.Commands;

public class CreateBetRequestHandler : IRequestHandler<CreateBetCommand, BaseCommandResponse<Guid>>
{
    private readonly IBetRepository _betRepository;
    private readonly IBetMapper _betMapper;

    public CreateBetRequestHandler(IBetMapper betMapper, IBetRepository betRepository)
    {
        _betMapper = betMapper;
        _betRepository = betRepository;
    }

    public async Task<BaseCommandResponse<Guid>> Handle(CreateBetCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateBetValidator();
        var validationResult = await validator.ValidateAsync(request.BetDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new BaseCommandResponse<Guid>
            {
                Errors = validationResult.Errors.Select(error => new KeyValuePair<string, string>(error.PropertyName, error.ErrorMessage)).ToDictionary(x => x.Key, x => x.Value),
                Success = false,
                Message = "Bet creation failed",
                Data = Guid.Empty
            };
        }
        var bet = _betMapper.DtoToBet(request.BetDto);
        var created = await _betRepository.AddAsync(bet);
        return new BaseCommandResponse<Guid>()
        {
            Data = created.Id,
            Success = true,
            Message = "Bet created successfully"
        };
    }
}
