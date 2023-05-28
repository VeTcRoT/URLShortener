namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        string GenerateShortUrl(string originalUrl);
    }
}
