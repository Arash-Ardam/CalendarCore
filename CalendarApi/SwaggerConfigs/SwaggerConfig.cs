using CalendarDbContext.DbConfigs;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CalendarRestApi.SwaggerConfigs
{
    public static class SwaggerConfig
    {

        public static void AddSwaggerGenConfigs(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetRequiredSection("Swagger");
            var swaggerOptions = config.Get<SwaggerOptions>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v2",
                    Title = $"{swaggerOptions.ProductName} API",
                    Contact = new OpenApiContact
                    {
                        Name = swaggerOptions.ContactName,
                        Email = swaggerOptions.ContactEmail
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }
                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }
                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
            });
            
        }

    }
}
