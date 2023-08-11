using Microsoft.Extensions.Logging;

namespace VocabularySheet.Application.Commons.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeRequest = typeof(TRequest);

        string requestName = typeRequest.DeclaringType?.Name != null ? $"{typeRequest.DeclaringType.Name}+{typeRequest.Name}" : $"{typeRequest.Name}";

        _logger.LogInformation("Starting request {@RequestName}, {@DataTimeUtc}",
            requestName,
            DateTime.UtcNow);


        try
        {
            var result = await next();

 

            return result;

        }
        catch(Exception ex) 
        {
            _logger.LogInformation("Request failure {@RequestName}, {@Error} , {@DataTimeUtc}",
                 requestName,
                ex.Message,
                DateTime.UtcNow);

            throw;
        }
        finally
        {
            _logger.LogInformation("Complete request {@RequestName}, {@DataTimeUtc}",
                requestName,
                DateTime.UtcNow);
        }
    }
}