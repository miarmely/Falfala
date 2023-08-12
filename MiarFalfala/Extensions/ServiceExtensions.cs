using Entities.ConfigModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes;
using Repositories.Contracts;
using Repositories.EF;
using Services.Concretes;
using Services.Contracts;


namespace Falfala.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration
                    .GetConnectionString("SqlServer")));


        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<MailSettingsConfig>();
        }
            

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureMailSettingsConfig(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<MailSettingsConfig>(configuration
                .GetSection(nameof(MailSettingsConfig)));
    }
}
