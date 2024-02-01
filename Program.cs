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
    .AddSwaggerGen();

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddDbContext<URLShortenerDbContext> (option => option
    .UseSqlServer(builder.Configuration
    .GetConnectionString("Default")));

builder.Services
    .AddTransient<IURLShortenerService, URLShortenerService>();

var app = builder
    .Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
