using System.Net;
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
                    [..8];

                return hash;
            }
        }

        public async Task<bool> CheckUrlAsync(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Method = "HEAD";

                using var response = await request.GetResponseAsync();
                return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
