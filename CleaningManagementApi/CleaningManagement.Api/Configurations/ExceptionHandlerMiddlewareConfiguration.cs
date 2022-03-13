using CleaningManagement.Api.Middleware.ExceptionHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api.Configurations
{
    public static class ExceptionHandlerMiddlewareConfiguration
    {
        public static void AddExceptionHandlerMiddleware(this IServiceCollection services)
        {
            services.AddTransient<IExceptionToHttpResponseParametersMapper, ExceptionToHttpResponseParametersMapper>();
        }

        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
