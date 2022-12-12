namespace Whs.WebApi.Middleware
{
    public static class CustomMiddlewareExtemsions
    {
        public static IApplicationBuilder UseCustomEcxeptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddlware>();
        }
    }
}
