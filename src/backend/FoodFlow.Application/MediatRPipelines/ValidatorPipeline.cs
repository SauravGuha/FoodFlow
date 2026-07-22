
using FluentValidation;
using MediatR;

namespace FoodFlow.Application.MediatRPipelines;

public class ValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidatorPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = validators.Select(v => v.Validate(context)).ToList();
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
        return next();
    }
}