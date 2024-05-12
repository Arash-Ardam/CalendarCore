namespace CalendarRestApi.ExceptionHandling.Comfigs
{
    public static class ExceptionConfigs
    {

        public static void AddRequiredExceptionHandlers(this WebApplicationBuilder builder) 
        {
            builder.Services.AddExceptionHandler<CustomExceptionsHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionsHandler>();
        }
    }
}
