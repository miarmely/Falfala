using Entities.ErrorModels;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Text.Json;


namespace Falfala_MVC.Extensions
{
    public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigureExceptionHandler(this WebApplication app)
		{
			app.UseExceptionHandler(configure =>
			{
				configure.Run(async context =>
				{
					#region get logger service
					var loggerService = context.RequestServices
						.GetRequiredService<ILoggerService>();
					#endregion

					#region get errors
					var contextFeature = context.Features
						.Get<IExceptionHandlerFeature>();
					#endregion

					#region return response
					// if contextFeture is null then error not exits.
					if (contextFeature != null)
					{
						// deserialize errorDatails
						var errorDetails = JsonSerializer
							.Deserialize<ErrorDetails>(contextFeature.Error.Message);

						// configure response
						context.Response.ContentType = "application/json";
						context.Response.StatusCode = errorDetails.StatusCode;

						// save log
						loggerService.LogInfo(errorDetails.Message);

						// return response
						await context.Response.WriteAsJsonAsync(errorDetails);
					}
					#endregion
				});
			});
		}

	}
}
