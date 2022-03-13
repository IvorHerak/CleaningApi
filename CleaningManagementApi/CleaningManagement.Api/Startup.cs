using CleaningManagement.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleaningManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExceptionHandlerMiddleware();
            services.AddAnyOriginCors();
            services.AddControllers();
            services.AddEFCoreWithRepositories();
            services.AddBusinessServices();
            services.AddApiMappersServices();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAnyOriginCors();
            app.UseRouting();
            app.UseExceptionHandlerMiddleware();
            app.UseSwaggerWithUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
