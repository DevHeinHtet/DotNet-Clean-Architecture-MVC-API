using Application.Exceptions;
using Domain.Exceptions;
using Newtonsoft.Json;

namespace WebApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly Logger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next
            /*Logger<ExceptionHandlingMiddleware> logger*/)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, $"Exception Occured: {context.Request.Path}", exception.Message);
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorDetails = new
            {
                status = GetStatusCode(exception),
                title = GetTitle(exception),
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            context.Response.StatusCode = errorDetails.status;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };
        }

        private string GetTitle(Exception exception)
        {
            return exception switch
            {
                NotFoundException notFoundException => notFoundException.Title,
                ValidationException validationException => validationException.Title,
                _ => "Server Failure",
            };
        }

        private IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}
