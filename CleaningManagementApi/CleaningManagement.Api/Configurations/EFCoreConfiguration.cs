using CleaningManagement.DAL;
using CleaningManagement.DAL.Entities.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api.Configurations
{
    public static class EFCoreConfiguration
    {
        public static void AddEFCoreWithRepositories(this IServiceCollection services)
        {
            services.AddDbContext<CleaningManagementDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
