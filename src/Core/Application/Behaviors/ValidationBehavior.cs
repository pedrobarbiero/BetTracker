using Application.Responses;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, new()
    where TResponse : BaseCommandResponse
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(result => result.Errors).Where(error => error != null).ToList();

        if (failures.Any())
        {
            return new BaseCommandResponse()
            {
                Success = false,
                Message = "Validation errors",
                Errors = failures.Select(f => new KeyValuePair<string, string>(f.PropertyName, f.ErrorMessage)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            } as TResponse;
        }

        return await next();
    }

}
