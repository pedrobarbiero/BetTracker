using Application.Dtos.Bankroll;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Mappers.Contracts;
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.Implementation;

[Mapper]
public partial class BankrollMapper : IBankrollMapper
{
    public partial GetBankrollDto GetBankrollDto(Bankroll bankroll);
    public partial Bankroll ToBankroll(CreateBankrollCommand command);
}
