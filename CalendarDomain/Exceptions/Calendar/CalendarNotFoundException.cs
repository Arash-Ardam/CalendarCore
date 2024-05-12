using DataStructures.Exceptions;
using System.Net.Http;
using System.Net;
using System;
using System.Runtime.Serialization;
using System.Threading;

namespace CalendarDomain.Exceptions.Calendar
{
    public class CalendarNotFoundException : DomainException
    {
        public static string MessageTemplate = "Calander with name : ( {0} ) not found.";

        public string CalendarName { get; private set; }

        public CalendarNotFoundException()
        {
        }

        public CalendarNotFoundException(string calendarName) : base(string.Format(MessageTemplate, calendarName))
        {
            CalendarName = calendarName;
        }

        //public CalendarNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        //{
        //}

        //protected CalendarNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}



        //public static void HandleExceptions(ProblemDetails problemDetails ,
        //    Exception exception,
        //    CancellationToken cancellationToken)
        //{
        //    if (exception is ArgumentNullException)
        //    {
        //        problemDetails = new ProblemDetails()
        //        {
        //            Status = (int)HttpStatusCode.BadRequest,
        //            Title = exception.GetType().Name,
        //            Detail = $"There is no Entity with Args : {httpContext.Request.RouteValues.ToList()}"
        //        };
        //    }

        //    else if (exception is DbUpdateException)
        //    {
        //        var sqlException = exception.InnerException as SqlException;
        //        if (sqlException.Number == 2627)
        //        {
        //            problemDetails = new ProblemDetails()
        //            {
        //                Status = (int)HttpStatusCode.BadRequest,
        //                Type = exception.GetType().Name,
        //                Title = $"Entity with Arg ({httpContext.Request.Path.Value}) is already Exist",
        //                Detail = exception.Message
        //            };
        //        }
        //    }
        //    else
        //    {
        //        problemDetails = new ProblemDetails()
        //        {
        //            Status = (int)HttpStatusCode.InternalServerError,
        //            Type = exception.GetType().Name,
        //            Title = "An unhandled error occurred",
        //            Detail = exception.Message
        //        };
        //    }
        //    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        //}

    }
}
