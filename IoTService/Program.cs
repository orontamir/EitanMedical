using IoTService.Interfaces;
using IoTService.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IIotService, IotService>();
builder.Services.AddSingleton<IIOService, IOService>();
builder.Services.AddHostedService<MainService>();
builder.Host.UseSerilog(((context, services) =>
{
    var config = new ConfigurationBuilder()
       .AddEnvironmentVariables()
       .Build();
    var template = "{Timestamp:yyyy-MM-dd HH:mm:ss}  {Level:u4}  {Message:lj}{NewLine}{Exception}";
    services
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: template)
        .WriteTo.Debug(outputTemplate: template)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .ReadFrom.Configuration(context.Configuration);

    var logFolder = context.Configuration["LOG_FOLDER"] ?? "Log";
    var logFile = Path.Combine(logFolder, "IoTService.log");
    services.WriteTo.File(logFile, outputTemplate: template, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10000000);
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IoT Service",
        Version = "v1",
        Description = "IoT Service  - REST API",
        Contact = new OpenApiContact
        {
            Name = "Oron",
        }
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
