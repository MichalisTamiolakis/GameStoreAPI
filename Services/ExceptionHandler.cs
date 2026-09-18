using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exceptionMessage);

            // Handle exception and return 400 or 500
            var details = new ProblemDetails
            {
                Status = exception switch
                {
                    ApplicationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                },
                Title = exception switch
                {
                    ApplicationException => "Bad Request",
                    _ => "An unexpected error occurred"
                }
            };


            httpContext.Response.StatusCode = details.Status.Value;
            
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

            // Mark exception as handled
            return true;
        }
    }
}
