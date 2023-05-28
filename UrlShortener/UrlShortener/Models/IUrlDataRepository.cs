using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public interface IUrlDataRepository
    {
        Task<UrlData?> GetByIdAsync(int id);
        Task<IEnumerable<UrlData>> GetAllAsync();
        Task DeleteAsync(UrlData urlData);
        Task AddAsync(UrlData urlData);
        Task<UrlData?> GetByOriginalUrl(string originalUrl);
        Task<UrlData?> GetByShortUrl(string shortUrl);
        Task<IEnumerable<UrlData>> GetUserUrls(IdentityUser user);
    }
}
