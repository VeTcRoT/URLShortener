using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public class UrlData
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public IdentityUser User { get; set; } = null!;
    }
}
