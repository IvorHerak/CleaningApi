using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api.Configurations
{
    public static class CorsConfiguration
    {
        public const string AllowAnyOriginPolicyName = "AllowAnyOrigin";

        public static void AddAnyOriginCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
        }

        public static void UseAnyOriginCors(this IApplicationBuilder app)
        {
            app.UseCors(AllowAnyOriginPolicyName);
        }
    }
}
