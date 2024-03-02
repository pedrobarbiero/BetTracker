using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Domain.Models;

namespace Application.Mappers.Contracts;

public interface IBankrollMapper
{
    public Bankroll ToBankroll(CreateBankrollCommand command);
    public Bankroll ToBankroll(UpdateBankrollCommand command);
    public GetBankrollDto GetBankrollDto(Bankroll bankroll);
}
