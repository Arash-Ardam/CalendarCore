using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TPG.SI.SMS.Api.Configurations.Authorization;

namespace CalendarRestApi.IdentityConfigs
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfig(this WebApplicationBuilder webApplicationBuilder)
        {
            var configurationSection = webApplicationBuilder.Configuration.GetRequiredSection("AppConfigurations");
            var appConfigurations = configurationSection.Get<AppConfigurations>();
            webApplicationBuilder.Services.AddSingleton(appConfigurations);


            webApplicationBuilder
             .Services.AddAccessTokenManagement().ConfigureBackchannelHttpClient(client =>
             {
                 client.Timeout = TimeSpan.FromSeconds(30.0);
             });
            webApplicationBuilder
                .Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                  .AddOAuth2Introspection("introspection", options =>
                  {
                      options.Authority = appConfigurations.IdentityAddress;
                      options.ClientId = appConfigurations.OidcApiName;
                      options.ClientSecret = appConfigurations.OidcSwaggerUIClientSecret;
                      options.EnableCaching = false;
                  })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = appConfigurations.IdentityAddress;
                    options.RequireHttpsMetadata = appConfigurations.RequireHttpsMetadata;
                    options.Audience = appConfigurations.IdentityAudience;
                    options.MapInboundClaims = true;

                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    options.ForwardDefaultSelector = SchemeSelector.ForwardReferenceToken("introspection");
                });

            webApplicationBuilder.Services.AddAuthorization(options =>
            {

                options.AddPolicy(AuthorizationConsts.CalendarAdminRole,
                                    policy =>
                                        policy.RequireAssertion(context => context.User.HasClaim(c =>
                                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.CalendarAdminRole) ||
                                                 (c.Type == AuthorizationConsts.RoleClaimType && c.Value == AuthorizationConsts.CalendarAdminRole)))
                                        ));

            });

        }
    }
}
