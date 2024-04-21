using CalendarDomain.Exceptions.Calendar;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CalendarRestApi.ExceptionHandling
{
    public class CalendarExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CalendarExceptionHandler> logger;
        public CalendarExceptionHandler(ILogger<CalendarExceptionHandler> logger)
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

            if (exception is CalendarNotFoundException)
            {
                var typedExceptrion = exception as CalendarNotFoundException;
                problemDetails = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = exception.GetType().Name,
                    //Detail = $"Calander {httpContext.Request.RouteValues.ToList()}"
                    Detail = typedExceptrion.Message
                };

                isHandled = true;
            }
            else if (exception is EventNotFoundException)
            {
                var typedExceptrion = exception as EventNotFoundException;
                problemDetails = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = exception.GetType().Name,
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
