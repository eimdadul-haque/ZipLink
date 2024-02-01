using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.IService;
using URL_Shortener.Services;

var builder = WebApplication
    .CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();

builder.Services
    .AddDbContext<URLShortenerDbContext> (option => option
    .UseSqlServer(builder.Configuration
    .GetConnectionString("Default")));

builder.Services
    .AddTransient<IURLShortenerService, URLShortenerService>();

var app = builder
    .Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
