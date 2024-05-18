using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdminPanel.IdentityConfigs
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfig(this WebApplicationBuilder webApplicationBuilder)
        {
            var configurationSection = webApplicationBuilder.Configuration.GetRequiredSection("AppConfigurations");
            var appConfigurations = configurationSection.Get<AppConfigurations>();
            webApplicationBuilder.Services.AddSingleton(appConfigurations);

            webApplicationBuilder
                .Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = appConfigurations.IdentityAddress;
                    options.RequireHttpsMetadata = appConfigurations.RequireHttpsMetadata;
                    options.Audience = appConfigurations.IdentityAudience;
                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                    };
                });

            webApplicationBuilder.Services.AddAuthorization();
            //webApplicationBuilder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
            //                    policy =>
            //                        policy.RequireAssertion(context => context.User.HasClaim(c =>
            //                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.AdminRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.AdminRole) ||
            //                                 (c.Type == JwtClaimTypes.Scope && c.Value == AuthorizationConsts.AdminScope)))
            //                        ));

            //    options.AddPolicy(AuthorizationConsts.CartableAdminPolicy,
            //                    policy =>
            //                        policy.RequireAssertion(context => context.User.HasClaim(c =>
            //                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.CartableAdminRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.CartableAdminRole) ||
            //                                 (c.Type == JwtClaimTypes.Scope && c.Value == AuthorizationConsts.CartableApiScope)))
            //                        ));

            //    options.AddPolicy(AuthorizationConsts.CartableApproverPolicy,
            //                    policy =>
            //                        policy.RequireAssertion(context => context.User.HasClaim(c =>
            //                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.CartableApproverRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.CartableApproverRole) ||
            //                                 (c.Type == JwtClaimTypes.Scope && c.Value == AuthorizationConsts.CartableApiScope)))
            //                        ));

            //    options.AddPolicy(AuthorizationConsts.CartableUserPolicy,
            //                    policy =>
            //                        policy.RequireAssertion(context => context.User.HasClaim(c =>
            //                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.CartableApproverRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.CartableAdminRole) ||
            //                                 (c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.CartableAdminRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.CartableApproverRole) ||
            //                                 (c.Type == JwtClaimTypes.Scope && c.Value == AuthorizationConsts.CartableApiScope)))
            //                        ));

            //    options.AddPolicy(AuthorizationConsts.WithdrawalClientPolicy,
            //                    policy =>
            //                        policy.RequireAssertion(context => context.User.HasClaim(c =>
            //                                ((c.Type == JwtClaimTypes.Role && c.Value == AuthorizationConsts.WithdrawalClientRole) ||
            //                                 (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == AuthorizationConsts.WithdrawalClientRole) ||
            //                                 (c.Type == JwtClaimTypes.Scope && c.Value == AuthorizationConsts.WithdrawalScope)))
            //                        ));

            //});
        }
    }
}
