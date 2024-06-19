using CalendarDomain.Exceptions.Calendar;
using CalendarRestApi.Problems;
using DataStructures.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace CalendarRestApi.ExceptionHandling
{
    public class CustomExceptionsHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionsHandler> logger;
        public CustomExceptionsHandler(ILogger<CustomExceptionsHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails? problem = default;

            //try
            //{
            //    //var domainExceptionTypes = System.Reflection.Assembly
            //    //    .GetEntryAssembly()
            //    //    .GetReferencedAssemblies()
            //    //    .Select(Assembly.Load)
            //    //    .Select(ass => ass.DefinedTypes)
            //    //    //.SelectMany(module => module.GetTypes())
            //    //    //.Select(typeinfo =>  typeinfo.DeclaringType)
            //    //    .Distinct()
            //    //    //.Where(t => typeof(DomainException).IsAssignableFrom(t))
            //    //    .ToList();

            //    //var domainExceptionTypes = typeof(DomainException)
            //    //    .Assembly
            //    //    .DefinedTypes
            //    //    .Where(typeInfo => typeInfo.IsSubclassOf(typeof(DomainException)))
            //    //    .ToList ();

            //var domainExceptionTypes = AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(ass => ass.GetTypes())
            //    .ToList();
            //try 
            //{ 
            //    var domainExceptionTypes = AssemblyHelper.GetAllAssemblies("DataStructures").SelectMany(ass => ass.GetTypes());
            //}
            //catch (Exception ex)
            //{
            
            //}
            
    List<Type> exceptionTypes = new List<Type>()
            {
                 typeof(AffectedDateIsLessThanMinException),
                 typeof(CalendarNotFoundException),
                 typeof(EventNotFoundException),
            };
            

            foreach (var item in exceptionTypes)
            {
                if (exception.GetType() == item)
                {
                    problem = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = nameof(exception),
                        Detail = exception.Message
                    };
                    break;
                }
            }

            var isHandled = default == problem;
            if (isHandled)
            {
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
                logger.LogError(problem.Detail);

            }
            return isHandled;
        }
    }
}
