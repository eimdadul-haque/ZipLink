using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<URLShortenerDbContext>((option) =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(""));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
