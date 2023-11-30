using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Domain;

namespace Application.Mappers.Contracts;

public interface IBankrollMapper
{
    public Bankroll ToBankroll(CreateBankrollCommand command);
    public GetBankrollDto GetBankrollDto(Bankroll bankroll);
}
