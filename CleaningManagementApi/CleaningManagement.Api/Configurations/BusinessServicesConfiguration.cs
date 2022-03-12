using CleaningManagement.BLL.CleaningPlans.Models;
using CleaningManagement.BLL.CleaningPlans.Services;
using CleaningManagement.BLL.CleaningPlans.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api.Configurations
{
    public static class BusinessServicesConfiguration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICleaningPlanService, CleaningPlanService>();
            services.AddScoped<IValidator<CreateCleaningPlanRequest>, CreateCleaningPlanRequestValidator>();
            services.AddScoped<IValidator<UpdateCleaningPlanRequest>, UpdateCleaningPlanRequestValidator>();
        }
    }
}
