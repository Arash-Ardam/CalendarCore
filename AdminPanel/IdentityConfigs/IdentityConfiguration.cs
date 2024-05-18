using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = "oidc";
                    options.DefaultSignOutScheme = "oidc";
                })
                .AddCookie("Cookies", options =>
                {
                    // host prefixed cookie name
                    options.Cookie.Name = "Calendar_Api_Cookie";

                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    options.Authority = appConfigurations.IdentityAddress;
                    options.CallbackPath = "/auth/auth-callback";
                    options.RequireHttpsMetadata = false;
                    options.SignedOutRedirectUri = "/";

                    // confidential client using code flow + PKCE
                    options.ClientId = appConfigurations.OidcSwaggerUIClientId;
                    options.ClientSecret = appConfigurations.OidcSwaggerUIClientSecret;
                    options.ResponseType = OidcConstants.ResponseTypes.Code;
                    
                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    // request scopes + refresh tokens
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("calendar.api.scope");
                    //options.Scope.Add("offline_access");

                    options.ClaimActions.MapAll();
                    options.ClaimActions.MapJsonKey("role", "role");
                    options.ClaimActions.MapJsonKey("tenant", "tenant");

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;


                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //ValidateAudience = true,
                        //ValidTypes = new[] { "at+jwt" },
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };

                    options.Events.OnRedirectToIdentityProvider = ctx =>
                    {
                        // ctx.ProtocolMessage.Prompt = "create";
                        return Task.CompletedTask;
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
