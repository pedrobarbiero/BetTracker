using Application.Features.Bankrolls.Requests.Commands;
using FluentValidation;

namespace Application.Features.Bankrolls.Validators;

public class CreateBankrollCommandValidator : AbstractValidator<CreateBankrollCommand>
{
    public CreateBankrollCommandValidator()
    {
        RuleFor(t => t).SetValidator(new BankrollValidator());
    }
}
