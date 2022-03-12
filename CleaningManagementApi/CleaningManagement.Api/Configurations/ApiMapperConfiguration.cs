using CleaningManagement.Api.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api.Configurations
{
    public static class ApiMapperConfiguration
    {
        public static void AddApiMappersServices(this IServiceCollection services)
        {
            services.AddScoped<ICleaningPlanMapper, CleaningPlanMapper>();
        }
    }
}
