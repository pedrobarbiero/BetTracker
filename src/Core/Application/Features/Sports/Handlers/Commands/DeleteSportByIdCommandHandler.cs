using Application.Contracts.Persistence;
using Application.Features.Sports.Requests.Commands;
using Application.Responses;
using MediatR;

namespace Application.Features.Sports.Handlers.Commands;

public class DeleteSportByIdCommandHandler : IRequestHandler<DeleteSportByIdCommand, BaseCommandResponse>
{
    private readonly ISportRepository _sportRepository;

    public DeleteSportByIdCommandHandler(ISportRepository sportRepository)
    {
        _sportRepository = sportRepository;
    }


    public async Task<BaseCommandResponse> Handle(DeleteSportByIdCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _sportRepository.DeleteAsync(request.Id);
        if (!deleted)
        {
            return new BaseCommandResponse()
            {
                Id = request.Id,
                Success = false,
                Message = "Sport not found.",
            };
        }
        return new BaseCommandResponse()
        {
            Success = true,
            Message = "Sport deleted succesfully"
        };
    }
}
