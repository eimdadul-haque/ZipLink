using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using URL_Shortener.Data;
using URL_Shortener.IService;
using URL_Shortener.Middlewares;
using URL_Shortener.Services;

var builder = WebApplication
    .CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
    .MinimumLevel.Information()
#endif
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(
        "logs/.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();

builder.Host
    .UseSerilog();

builder.Services
    .AddControllers();

builder.Services
    .AddSwaggerGen();

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddDbContext<URLShortenerDbContext>(option => option
    .UseSqlServer(builder.Configuration
    .GetConnectionString("Default")));

builder.Services
    .AddTransient<IURLShortenerService, URLShortenerService>();

var app = builder
    .Build();

// Configure the HTTP request pipeline.

app.UseSwaggerUI(options
    => options.SwaggerEndpoint("v1/swagger.json", "v1"));

app.UseMiddleware<GlobalErrorHandler>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.Run();
