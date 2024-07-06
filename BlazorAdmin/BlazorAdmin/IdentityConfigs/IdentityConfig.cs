//using IdentityModel;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.IdentityModel.Tokens;

//namespace BlazorAdmin.IdentityConfigs
//{
//    public static class IdentityConfig
//    {
//        public static void AddIdentityConfig(this WebApplicationBuilder webApplicationBuilder)
//        {
        

//            webApplicationBuilder
//                .Services.AddAuthentication(options =>
//                {
//                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                    options.DefaultChallengeScheme = "oidc";
//                    options.DefaultSignOutScheme = "oidc";
//                })
//                .AddCookie("Cookies", options =>
//                {
//                    // host prefixed cookie name
//                    options.Cookie.Name = "Calendar_Api_Cookie";

//                })
//                .AddOpenIdConnect("oidc", options =>
//                {
//                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

//                    options.Authority = appConfigurations.IdentityAddress;
//                    options.CallbackPath = "/auth/auth-callback";
//                    options.RequireHttpsMetadata = false;
//                    options.SignedOutRedirectUri = "/";

//                    // confidential client using code flow + PKCE
//                    options.ClientId = appConfigurations.OidcSwaggerUIClientId;
//                    options.ClientSecret = appConfigurations.OidcSwaggerUIClientSecret;
//                    options.ResponseType = OidcConstants.ResponseTypes.Code;

//                    options.MapInboundClaims = false;
//                    options.GetClaimsFromUserInfoEndpoint = true;
//                    options.SaveTokens = true;

//                    // request scopes + refresh tokens
//                    options.Scope.Clear();
//                    options.Scope.Add("openid");
//                    options.Scope.Add("profile");
//                    options.Scope.Add("calendar.api.scope");
//                    //options.Scope.Add("offline_access");

//                    options.ClaimActions.MapAll();
//                    options.ClaimActions.MapJsonKey("role", "role");
//                    options.ClaimActions.MapJsonKey("tenant", "tenant");

//                    options.GetClaimsFromUserInfoEndpoint = true;
//                    options.SaveTokens = true;


//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        //ValidateAudience = true,
//                        //ValidTypes = new[] { "at+jwt" },
//                        NameClaimType = JwtClaimTypes.Name,
//                        RoleClaimType = JwtClaimTypes.Role,
//                    };

//                    options.Events.OnRedirectToIdentityProvider = ctx =>
//                    {
//                        // ctx.ProtocolMessage.Prompt = "create";
//                        return Task.CompletedTask;
//                    };

//                });

//            webApplicationBuilder.Services.AddAuthorization();
//        }
//        }



//    public sealed class AppConfigurations
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public string ApiName { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public string ApiVersion { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public string IdentityAddress { get; set; } = string.Empty;
//        public string IdentityApiBaseUrl { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public string IdentityAudience { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public string OidcSwaggerUIClientId { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public string OidcSwaggerUIClientSecret { get; set; } = string.Empty;


//        /// <summary>
//        /// 
//        /// </summary>
//        public string OidcApiName { get; set; } = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        public bool RequireHttpsMetadata { get; set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        public bool CorsAllowAnyOrigin { get; set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        public string[]? CorsAllowOrigins { get; set; }
//    }

//}
