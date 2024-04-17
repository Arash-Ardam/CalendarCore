using CalendarDbContext.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarDbContext.DbConfigs
{
    public static class CalendarDbConfig
    {
        public static void AddCalendarDb(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetRequiredSection("DbOptions");
            var dbOptions = config.Get<DbOptions>();
            if (default == dbOptions)
            {
                throw new NullReferenceException(nameof(dbOptions));
            }

            if (dbOptions.isEnabled)
            {
                services.AddDbContext<ApplicationDbContext>(x =>
                    x.UseSqlServer( 
                        dbOptions.connectionString
                        ,sqlServerOptionsAction =>
                        {
                            sqlServerOptionsAction.EnableRetryOnFailure(
                                maxRetryCount : dbOptions.maxRetry
                                );

                        }),ServiceLifetime.Scoped
                        );
            }

        }
    }
}
