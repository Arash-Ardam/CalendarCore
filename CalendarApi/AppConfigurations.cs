namespace CalendarRestApi
{

    public sealed class AppConfigurations
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ApiVersion { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string IdentityAddress { get; set; } = string.Empty;
        public string IdentityApiBaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string IdentityAudience { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string OidcSwaggerUIClientId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string OidcSwaggerUIClientSecret { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        public string OidcApiName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CorsAllowAnyOrigin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[]? CorsAllowOrigins { get; set; }
    }
}
