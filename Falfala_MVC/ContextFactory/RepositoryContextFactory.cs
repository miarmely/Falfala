using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EF;


namespace Falfala_MVC.ContextFactory
{
	public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
	{
		public RepositoryContext CreateDbContext(string[] args)
		{
			// set configuration
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory
					.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// set dbContextOptions
			var options = new DbContextOptionsBuilder()
				.UseSqlServer(
					configuration.GetConnectionString("SqlServer")
					, prj => prj.MigrationsAssembly("Falfala_MVC"))
				.Options;

			return new RepositoryContext(options);
		}
	}
}
