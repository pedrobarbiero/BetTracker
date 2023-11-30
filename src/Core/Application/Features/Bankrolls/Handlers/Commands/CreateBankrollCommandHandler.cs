using Application.Contracts.Persistence;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Commands;

public class CreateBankrollCommandHandler : IRequestHandler<CreateBankrollCommand, BaseCommandResponse>
{
    private readonly IBankrollMapper _bankrollMapper;
    private readonly IBankrollRepository _bankrollRepository;

    public CreateBankrollCommandHandler(IBankrollMapper bankrollMapper, IBankrollRepository bankrollRepository)
    {
        _bankrollMapper = bankrollMapper;
        _bankrollRepository = bankrollRepository;
    }

    public async Task<BaseCommandResponse> Handle(CreateBankrollCommand request, CancellationToken cancellationToken)
    {
        var bankroll = _bankrollMapper.ToBankroll(request);
        var created = await _bankrollRepository.AddAsync(bankroll);
        return new BaseCommandResponse()
        {
            Id = created.Id,
            Success = true,
            Message = "Bankroll created successfully"
        };
    }
}
