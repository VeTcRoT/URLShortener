using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;

namespace UrlShortener.Models
{
    public class UrlDataRepository : IUrlDataRepository
    {
        private readonly UrlShortenerDbContext _dbContext;

        public UrlDataRepository(UrlShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UrlData urlData)
        {
            await _dbContext.UrlDatas.AddAsync(urlData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UrlData urlData)
        {
            _dbContext.UrlDatas.Remove(urlData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UrlData>> GetAllAsync()
        {
            return await _dbContext.UrlDatas.Include(u => u.User).ToListAsync();
        }

        public async Task<UrlData?> GetByIdAsync(int id)
        {
            return await _dbContext.UrlDatas.Include(u => u.User).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UrlData?> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _dbContext.UrlDatas.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
        }

        public async Task<UrlData?> GetByShortUrlAsync(string shortUrl)
        {
            return await _dbContext.UrlDatas.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
        }

        public async Task<IEnumerable<UrlData>> GetUserUrlsAsync(IdentityUser user)
        {
            return await _dbContext.UrlDatas.Where(u => u.User == user).ToListAsync();
        }
    }
}
