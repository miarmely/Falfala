using Entities.ConfigModels;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.Concretes;
using Repositories.Contracts;
using Repositories.EF;
using Services.Concretes;
using Services.Contracts;


namespace Falfala_MVC.Extension
{
	public static class ServiceExtensions
	{
		public static void AddRepositoryContext(this IServiceCollection services, IConfiguration configuration) =>
			services.AddDbContext<RepositoryContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("SqlServer")
					, prj => prj.MigrationsAssembly("Falfala_MVC")));  // set migration location

		public static void ConfigureRepositoryManager(this IServiceCollection services) =>
			services.AddScoped<IRepositoryManager, RepositoryManager>();

		public static void ConfigureServiceManager(this IServiceCollection services) =>
			services.AddScoped<IServiceManager, ServiceManager>();

		public static void ConfigureLogger(this IServiceCollection services) =>
			services.AddSingleton<ILoggerService, LoggerService>();

		public static void AddControllersWithViewsAsConfigured(this IServiceCollection services) =>
			services.AddControllersWithViews()
				.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
				.AddRazorRuntimeCompilation();

		public static void ConfigureMailSettings(this IServiceCollection services
			, IConfiguration configuration) =>
			services.Configure<MailSettingsConfig>(configuration
				.GetSection(nameof(MailSettingsConfig)));

		public static void ConfigureUserSettings(this IServiceCollection services
			, IConfiguration configuration) =>
			services.Configure<UserSettingsConfig>(configuration
				.GetSection(nameof(UserSettingsConfig)));
	}
}
