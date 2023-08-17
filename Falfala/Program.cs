using Falfala.Extension;


var builder = WebApplication.CreateBuilder(args);

#region add service extensions
builder.Services.AddControllersWithViewsAsConfigured();
builder.Services.AddRepositoryContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
	app.UseHsts();

#region add middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=RefreshPassword}/{action=Index}/{id?}");
#endregion

app.Run();