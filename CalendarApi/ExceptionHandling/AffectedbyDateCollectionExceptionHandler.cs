using DataStructures.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CalendarRestApi.ExceptionHandling
{
    public class AffectedbyDateCollectionExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AffectedbyDateCollectionExceptionHandler> logger;
        public AffectedbyDateCollectionExceptionHandler(ILogger<AffectedbyDateCollectionExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            var isHandled = false;

            ProblemDetails problemDetails = new ProblemDetails();

            if (exception is AffectedDateIsLessThanMinException)
            {
                var typedExceptrion = exception as AffectedDateIsLessThanMinException;
                problemDetails = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = exception.GetType().Name,
                    //Detail = $"Calander {httpContext.Request.RouteValues.ToList()}"
                    Detail = typedExceptrion.Message
                };

                isHandled = true;
            }

            if (isHandled)
            {
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            }

            return isHandled;
        }
    }
}
