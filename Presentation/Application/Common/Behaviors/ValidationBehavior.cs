using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<TResponse, ValidationException>> where TRequest : IRequest<Result<TResponse, ValidationException>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<Result<TResponse, ValidationException>> Handle(TRequest request,
        RequestHandlerDelegate<Result<TResponse, ValidationException>> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                return new ValidationException(failures);
            }
        }
        return await next();
    }
}
