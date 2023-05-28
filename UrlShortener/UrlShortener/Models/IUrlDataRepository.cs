namespace UrlShortener.Models
{
    public interface IUrlDataRepository
    {
        Task<UrlData> GetByIdAsync(int id);
        Task<IEnumerable<UrlData>> GetAllAsync();
        Task DeleteAsync(UrlData urlData);
        Task AddAsync(UrlData urlData);
        Task<UrlData> GetByOriginalUrl(string originalUrl);
    }
}
