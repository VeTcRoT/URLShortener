using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        string GenerateShortUrl(string originalUrl);
        Task<bool> CheckUrlAsync(string url);
    }
}
