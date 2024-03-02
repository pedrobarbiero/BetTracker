using Application.Contracts.Persistence;
using Application.Features.Sports.Requests.Commands;
using FluentValidation;

namespace Application.Features.Sports.Validators;

public class UpdateSportCommandValidator : AbstractValidator<UpdateSportCommand>
{
    private readonly ISportRepository _sportRepository;
    public UpdateSportCommandValidator(ISportRepository sportRepository)
    {
        _sportRepository = sportRepository;


        RuleFor(t => t).SetValidator(new SportValidator());

        RuleFor(t => t.Id)
            .MustAsync(async (id, token) =>
            {
                if (id == null || Guid.Empty == id) return false;
                return await _sportRepository.Exists(id.Value);
            })
            .WithMessage("Sport does not exist or you do not have permission to update it.");
    }
}
