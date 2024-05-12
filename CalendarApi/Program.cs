using CalendarApplication.CalendarServices;
using CalendarDbContext.DbConfigs;
using CalendarRestApi.SwaggerConfigs;
using Mapster;
using MapsterMapper;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using CalendarRestApi.ExceptionHandling.Comfigs;
using CalendarDbContext.Repositories;
using CalendarRestApi.IdentityConfigs;
using Serilog;

namespace CalendarApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(option => {
                        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }); ;
            builder.AddIdentityConfig();
            //
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.AddSwagger();
            //builder.AddVersioning();
            builder.AddRequiredExceptionHandlers();

            builder.Services.AddProblemDetails();

            builder.Services.AddCalendarDb(builder.Configuration);

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("C:\\Users\\A.Ardam\\source\\repos\\TPG.SI.CalendarNew\\log.txt"
                , Serilog.Events.LogEventLevel.Error
                ).CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            //builder.Services.AddLog(builder.Configuration,builder.Environment);



            //Add Mapster Mapper
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            // register the mapper as Singleton service for my application
            var mapperConfig = new Mapper(typeAdapterConfig);
            builder.Services.AddSingleton<IMapper>(mapperConfig);
            //Add Services
            builder.Services.AddScoped<ICalendarService, CalendarService>();
            builder.Services.AddScoped<ICalendarRepository, CalendarEfSqlRepository>();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyMethod();

                                  });
            });

            builder.Services
                .AddHttpClient();

            var app = builder.Build();
         

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerGen();
            }
            //app.UseLog();
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseExceptionHandler();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();
            app.MapControllers();
            

            app.Run();
        }
    }
}
