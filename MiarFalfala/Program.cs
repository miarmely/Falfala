using Falfala.Extensions;


var builder = WebApplication.CreateBuilder(args);

// add service
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson();

// service extensions
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();

var app = builder.Build();

// when mode is  development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();