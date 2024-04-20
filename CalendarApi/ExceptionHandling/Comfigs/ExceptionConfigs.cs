namespace CalendarRestApi.ExceptionHandling.Comfigs
{
    public static class ExceptionConfigs
    {

        public static void AddRequiredExceptionHandlers(this WebApplicationBuilder builder) 
        {
            builder.Services.AddExceptionHandler<CalendarExceptionHandler>();
            builder.Services.AddExceptionHandler<AffectedbyDateCollectionExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
