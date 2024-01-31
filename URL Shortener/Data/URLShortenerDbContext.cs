using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models.Entities;

namespace URL_Shortener.Data
{
    public class URLShortenerDbContext : DbContext
    {
        public URLShortenerDbContext(DbContextOptions<URLShortenerDbContext> options) 
            : base(options) { }  
        
        public DbSet<Url> Urls { get; set; }
    }
}
