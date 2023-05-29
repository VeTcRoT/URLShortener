using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public interface IUrlDataRepository
    {
        Task<UrlData?> GetByIdAsync(int id);
        Task<IEnumerable<UrlData>> GetAllAsync();
        Task DeleteAsync(UrlData urlData);
        Task AddAsync(UrlData urlData);
        Task<UrlData?> GetByOriginalUrlAsync(string originalUrl);
        Task<UrlData?> GetByShortUrlAsync(string shortUrl);
        Task<IEnumerable<UrlData>> GetUserUrlsAsync(IdentityUser user);
    }
}
