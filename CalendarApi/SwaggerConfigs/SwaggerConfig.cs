//using Asp.Versioning.ApiExplorer;
//using Microsoft.Extensions.Options;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace CalendarRestApi.SwaggerConfigs;
//public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
//{
//    private readonly IApiVersionDescriptionProvider provider;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
//    /// </summary>
//    /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
//    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

//    /// <inheritdoc />
//    public void Configure(SwaggerGenOptions options)
//    {
//        // add a swagger document for each discovered API version
//        // note: you might choose to skip or document deprecated API versions differently
//        foreach (var description in provider.ApiVersionDescriptions)
//        {
//            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
//        }
//    }

//    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
//    {
//        var info = new OpenApiInfo()
//        {
//            Version = description.ApiVersion.ToString(),
//            Contact = new OpenApiContact()
//            {
//                Name = "تدبیر پرداز",
//                Url = new Uri("https://etadbirco.ir")
//            }
//        };
//        info.Title = "Calendar API V2";
//        info.Description = "مستند سرویس تقویم";


//        return info;
//    }
//}