using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models.Entities;
using URL_Shortener.Services;

namespace URL_Shortener.Data
{
    public class URLShortenerDbContext : DbContext
    {
        public URLShortenerDbContext(DbContextOptions<URLShortenerDbContext> options)
            : base(options) { }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(builder =>
            {
                builder
                .Property(s => s.ShortUrl)
                .HasMaxLength(URLShortenerService.maxUrlLenght);

                builder
                .HasIndex(s => s.Code)
                .IsUnique();
            });
        }
    }
}
