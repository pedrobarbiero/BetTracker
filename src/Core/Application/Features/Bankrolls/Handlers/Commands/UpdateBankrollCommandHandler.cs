using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Commands;

public class UpdateBankrollCommandHandler : IRequestHandler<UpdateBankrollCommand, BaseCommandResponse>
{
    private readonly IBankrollMapper _bankrollMapper;
    private readonly IBankrollRepository _bankrollRepository;
    private readonly IUserProvider _userProvider;
    public UpdateBankrollCommandHandler(IBankrollMapper bankrollMapper, IBankrollRepository bankrollRepository, IUserProvider userProvider)
    {
        _bankrollMapper = bankrollMapper;
        _bankrollRepository = bankrollRepository;
        _userProvider = userProvider;
    }

    public Task<BaseCommandResponse> Handle(UpdateBankrollCommand command, CancellationToken cancellationToken)
    {
        command.ApplicationUserId = _userProvider.GetCurrentUserId();
        var bankroll = _bankrollMapper.ToBankroll(command);
        _bankrollRepository.Update(bankroll);
        return Task.FromResult(new BaseCommandResponse()
        {
            Id = command.Id,
            Success = true,
            Message = "Bankroll updated successfully"
        });
    }
}
