using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Services
{
    public class ShortUrlService : IShortUrlService
    {
        public string GenerateShortUrl(string originalUrl)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] urlBytes = Encoding.UTF8.GetBytes(originalUrl);
                byte[] hashBytes = sha256.ComputeHash(urlBytes);
                string hash = Convert.ToBase64String(hashBytes)
                    .Replace("/", "_")
                    .Replace("+", "-")
                    .Substring(0, 8);

                return hash;
            }
        }
    }
}
