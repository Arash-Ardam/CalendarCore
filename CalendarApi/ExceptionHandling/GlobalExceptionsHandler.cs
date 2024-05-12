using CalendarDomain.Exceptions.Calendar;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CalendarRestApi.ExceptionHandling
{
    public class GlobalExceptionsHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
           HttpContext httpContext,
           Exception exception,
           CancellationToken cancellationToken)
        {

            ProblemDetails problemDetails = new ProblemDetails();

            if (exception is DbUpdateException)
            {
                var sqlException = exception.InnerException as SqlException;

                if (sqlException == null)
                {
                    problemDetails = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = exception.GetType().Name,
                        Title = "Remove Entity Error",
                        Detail = "Entity with Argument Entered is already Removed",
                    };

                }

                else if (sqlException.Number == 2627)
                {
                    problemDetails = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = exception.GetType().Name,
                        Title = "Add Entity Error",
                        Detail = "Entity with Argument Entered is already Exist",
                    };
                }

                else
                {

                    problemDetails = new ProblemDetails()
                    {
                        
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = exception.GetType().Name,
                        Title = "An unhandled error occurred",
                        Detail = exception.InnerException.Message,

                    };
                }
                
            }
            else
            {
                problemDetails = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "An unhandled error occurred",
                    Detail = exception.Message
                };
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }
    }
}
