﻿using Entities.ConfigModels;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
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
				options.UseSqlServer(configuration
					.GetConnectionString("SqlServer")));

		public static void ConfigureServiceManager(this IServiceCollection services) => 
			services.AddScoped<IServiceManager, ServiceManager>();

		public static void ConfigureRepositoryManager(this IServiceCollection services) =>
			services.AddScoped<IRepositoryManager, RepositoryManager>();

		public static void AddControllersWithViewsAsConfigured(this IServiceCollection services) =>
			services.AddControllersWithViews()
				.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
				.AddRazorRuntimeCompilation();

		public static void ConfigureMailSettings(this IServiceCollection services, IConfiguration configuration) =>
			services.Configure<MailSettingsConfig>(configuration
				.GetSection(nameof(MailSettingsConfig)));
	}
}
