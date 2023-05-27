﻿using Microsoft.EntityFrameworkCore;
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
            return await _dbContext.UrlDatas.ToListAsync();
        }

        public async Task<UrlData> GetByIdAsync(int id)
        {
            return await _dbContext.UrlDatas.FirstAsync(u => u.Id == id);
        }
    }
}