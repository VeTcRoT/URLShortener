namespace UrlShortener.ViewModels
{
    public class UrlDetailsViewModel
    {
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
