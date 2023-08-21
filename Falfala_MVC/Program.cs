using Falfala_MVC.Extension;
using Falfala_MVC.Extensions;
using NLog;


#region setup logger
LogManager.Setup()
	.LoadConfigurationFromFile();
#endregion

#region add service extensions
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViewsAsConfigured();
builder.Services.AddRepositoryContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLogger();
builder.Services.ConfigureMailSettings(builder.Configuration);
builder.Services.ConfigureUserSettings(builder.Configuration);
#endregion

#region add error extensions
var app = builder.Build();

app.ConfigureExceptionHandler();
#endregion

#region set production and staging mode
if (!app.Environment.IsDevelopment())
	app.UseHsts();
#endregion

#region add pipelines
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=refreshPassword}/{action=index}/{email}");
#endregion


app.Run();