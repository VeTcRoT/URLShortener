using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UrlShortenerDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<UrlData> UrlDatas { get; set; }
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options) { }
    }
}
