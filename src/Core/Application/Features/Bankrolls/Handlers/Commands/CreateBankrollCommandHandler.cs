using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Mappers.Contracts;
using Application.Responses;
using Domain.Models.Identity;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Commands;

public class CreateBankrollCommandHandler : IRequestHandler<CreateBankrollCommand, BaseCommandResponse>
{
    private readonly IBankrollMapper _bankrollMapper;
    private readonly IBankrollRepository _bankrollRepository;
    private readonly IIdProvider _idProvider;
    private readonly IUserProvider _userProvider;

    public CreateBankrollCommandHandler(IBankrollMapper bankrollMapper, IBankrollRepository bankrollRepository, IIdProvider idProvider, IUserProvider userProvider)
    {
        _bankrollMapper = bankrollMapper;
        _bankrollRepository = bankrollRepository;
        _idProvider = idProvider;
        _userProvider = userProvider;
    }

    public async Task<BaseCommandResponse> Handle(CreateBankrollCommand request, CancellationToken cancellationToken)
    {
        request.Id = _idProvider.GetNextIdIfEmptyOrNull(request.Id);
        request.UserId = _userProvider.GetCurrentUserId();
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
