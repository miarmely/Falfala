using Falfala_MVC.Extension;

var builder = WebApplication.CreateBuilder(args);

#region add service extension
builder.Services.AddControllersWithViewsAsConfigured();
builder.Services.AddRepositoryContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureMailSettings(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
	app.UseHsts();

#region add middlewares
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