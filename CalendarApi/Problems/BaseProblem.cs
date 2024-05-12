using CalendarDomain.Exceptions.Calendar;
using DataStructures.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System.Net;
using System.Reflection;

namespace CalendarRestApi.Problems
{
    public class BaseProblem<T> : ProblemDetails
    {
        public BaseProblem(string exeptionMessage)
        {
            Status = (int)HttpStatusCode.BadRequest;
            Title = typeof(T).Name;
            Detail = exeptionMessage;
        }

        public static ProblemDetails Create(string exceptiomMessage)
        {
            return new BaseProblem<T>(exceptiomMessage);
        }
    }


    public static class AssemblyHelper
    {
        // get all assemblies that I wanted
        // you may want to filter assemblies with a namespace start name
        public static Assembly[] GetAllAssemblies(string namespaceStartName = "")
        {
            var ctx = DependencyContext.Default;
            var names = ctx.RuntimeLibraries.SelectMany(rl => rl.GetDefaultAssemblyNames(ctx));

            if (!string.IsNullOrWhiteSpace(namespaceStartName))
            {
                //if (namespaceStartName.Substring(namespaceStartName.Length - 1, 1) != ".")
                //    namespaceStartName += ".";

                names = names.Where(x => x.FullName != null && x.FullName.StartsWith(namespaceStartName)).ToArray();
            }

            var assemblies = new List<Assembly>();
            foreach (var name in names)
            {
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch { } // skip
            }

            return assemblies.ToArray();
        }
    }

}
