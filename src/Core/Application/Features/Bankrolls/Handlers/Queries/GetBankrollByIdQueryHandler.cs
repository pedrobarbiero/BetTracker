using Application.Contracts.Persistence;
using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Queries;
using Application.Mappers.Contracts;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Queries;

public class GetBankrollByIdQueryHandler : IRequestHandler<GetBankrollByIdQuery, GetBankrollDto?>
{
    private readonly IBankrollRepository _bankrollRepository;
    private readonly IBankrollMapper _bankrollMapper;

    public GetBankrollByIdQueryHandler(IBankrollMapper bankrollMapper, IBankrollRepository bankrollRepository)
    {
        _bankrollMapper = bankrollMapper;
        _bankrollRepository = bankrollRepository;
    }

    public async Task<GetBankrollDto?> Handle(GetBankrollByIdQuery request, CancellationToken cancellationToken)
    {
        var bankroll = await _bankrollRepository.GetByIdAsync(request.Id);

        if (bankroll is null) return null;

        return _bankrollMapper.GetBankrollDto(bankroll);
    }
}
