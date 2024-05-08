using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CalendarRestApi.SwaggerConfigs;


public static class SwaggerConfiguration
{
    public static void AddSwagger(this WebApplicationBuilder webApplicationBuilder)
    {
        var configurationSection = webApplicationBuilder.Configuration.GetRequiredSection("AppConfigurations");
        var appConfigurations = configurationSection.Get<AppConfigurations>();
        webApplicationBuilder.Services.Configure<AppConfigurations>(configurationSection);
        webApplicationBuilder.Services.AddOptions<AppConfigurations>();
        //webApplicationBuilder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        webApplicationBuilder.Services.AddSwaggerGen(options =>
        {


            string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(xmlCommentsFullPath);


            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ClientKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });




            //options.AddSecurityDefinition("apiKeyAuth", new OpenApiSecurityScheme()
            //{
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey,
            //    Name = AuthorizationConsts.ApiKeyHeaderName,
            //    Scheme = "apikey",
            //    Description = "Input apikey to access this API",
            //});


            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{appConfigurations.IdentityAddress}/connect/authorize"),
                        TokenUrl = new Uri($"{appConfigurations.IdentityAddress}/connect/token"),
                        Scopes = new Dictionary<string, string> {
                               
                                { "calendar.api.scope", "" }
                            }
                    }
                }
            });
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

    }
    public static void UseSwaggerGen(this WebApplication webApplication)
    {
        using var serviceScope = webApplication.Services.CreateScope();
        var appConfigurations = serviceScope.ServiceProvider.GetRequiredService<IOptions<AppConfigurations>>().Value;


        webApplication.UseSwagger();
        webApplication.UseSwaggerUI(options =>
        {
            //var descriptions = webApplication.DescribeApiVersions();

            // build a swagger endpoint for each discovered API version
            //foreach (var description in descriptions.OrderByDescending(x => x.GroupName))
            //{
            //    var url = $"/swagger/{description.GroupName}/swagger.json";
            //    var name = description.GroupName.ToUpperInvariant();
            //    options.SwaggerEndpoint(url, name);
            //}
            options.InjectStylesheet("/swagger-ui/swagger-ui.css");
            options.InjectJavascript("/swagger-ui/custom.js");

            options.OAuthClientId(appConfigurations.OidcSwaggerUIClientId);
            options.OAuthAppName(appConfigurations.ApiName);
            options.OAuthUsePkce();
        });
    }

}

