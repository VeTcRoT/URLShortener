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
                    .Substring(0, 8);

                return hash;
            }
        }

        public bool CheckUrlAsync(string url)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
