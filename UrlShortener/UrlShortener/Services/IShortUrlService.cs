using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        string GenerateShortUrl(string originalUrl);
        bool CheckUrlAsync(string url);
    }
}
