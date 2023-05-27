using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UrlShortenerDbContext : DbContext
    {
        public DbSet<UrlData> UrlDatas { get; set; }
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options) { }
    }
}
