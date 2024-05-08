namespace CalendarRestApi.SwaggerConfigs
{
    public static class VersioningConfiguration
    {
        public static void AddVersioning(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddApiVersioning(
                        options =>
                        {
                            options.ReportApiVersions = true;
                        })
                    .AddMvc()
                    .AddApiExplorer(
                        options =>
                        {
                            options.GroupNameFormat = "'v'VVV";

                            options.SubstituteApiVersionInUrl = false;

                        });
        }
    }
}
