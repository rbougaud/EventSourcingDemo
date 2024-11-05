using Domain.Common;
using FluentValidation;
using MediatR;
using Serilog;
using System.Reflection;

namespace Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse, ValidationException>>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<Result<TResponse, ValidationException>> Handle(TRequest request,
        RequestHandlerDelegate<Result<TResponse, ValidationException>> next, CancellationToken cancellationToken)
    {
        //Request
        _logger.Information($"Handling {typeof(TRequest).Name}");
        Type myType = request!.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType!.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object? propValue = prop.GetValue(request, null);
            _logger.Information("{Property} : {@Value}", prop.Name, propValue);
        }
        var response = await next();

        LogResponse(response);
        return response;
    }

    private void LogResponse(Result<TResponse, ValidationException> response)
    {
        if (response.Success)
        {
            _logger.Information($"Handled {typeof(TResponse).Name}");
        }
        else
        {
            _logger.Error(response.Error.Message);
        }
    }
}
