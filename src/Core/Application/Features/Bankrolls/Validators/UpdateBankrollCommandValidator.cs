using Application.Contracts.Persistence;
using Application.Features.Bankrolls.Requests.Commands;
using FluentValidation;

namespace Application.Features.Bankrolls.Validators;

public class UpdateBankrollCommandValidator : AbstractValidator<UpdateBankrollCommand>
{
    private readonly IBankrollRepository _bankrollRepository;
    public UpdateBankrollCommandValidator(IBankrollRepository bankrollRepository)
    {
        _bankrollRepository = bankrollRepository;


        RuleFor(t => t).SetValidator(new BankrollValidator());

        RuleFor(t => t.Id)
            .MustAsync(async (id, token) =>
            {
                if (id == null || Guid.Empty == id) return false;
                return await _bankrollRepository.Exists(id.Value);
            })
            .WithMessage("Bankroll does not exist or you do not have permission to update it.");
    }
}
