using Application.Contracts.Persistence;
using Application.Features.Bankrolls.Requests.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.Bankrolls.Handlers.Commands;

public class DeleteBankrollByIdCommandHandler : IRequestHandler<DeleteBankrollByIdCommand, BaseCommandResponse>
{
    private readonly IBankrollRepository _bankrollRepository;

    public DeleteBankrollByIdCommandHandler(IBankrollRepository bankrollRepository)
    {
        _bankrollRepository = bankrollRepository;
    }


    public async Task<BaseCommandResponse> Handle(DeleteBankrollByIdCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _bankrollRepository.DeleteAsync(request.Id);
        if (!deleted)
        {
            return new BaseCommandResponse()
            {
                Id = request.Id,
                Success = false,
                Message = "Bankroll not found.",
            };
        }
        return new BaseCommandResponse()
        {
            Success = true,
            Message = "Bankroll deleted succesfully"
        };
    }
}
