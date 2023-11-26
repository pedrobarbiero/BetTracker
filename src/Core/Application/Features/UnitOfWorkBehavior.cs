using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features;

public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, new()
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = next();
        if (IsCommand())
            _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }

    private static bool IsCommand() => typeof(TRequest).Name.EndsWith("Command");

}
