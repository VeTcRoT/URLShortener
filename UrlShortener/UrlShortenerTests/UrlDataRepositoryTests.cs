using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortenerTests
{
    public class UrlDataRepositoryTests
    {
        private DbContextOptions<UrlShortenerDbContext> CreateNewContextOptions()
        {
            var databaseName = "TestDatabase_" + Guid.NewGuid().ToString("N");

            return new DbContextOptionsBuilder<UrlShortenerDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        [Fact]
        public async Task AddAsync_AddsUrlDataToDatabase()
        {
            // Arrange
            var urlData = new UrlData { Id = 1, OriginalUrl = "http://example.com" };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                var repository = new UrlDataRepository(context);

                // Act
                await repository.AddAsync(urlData);

                // Assert
                Assert.Equal(1, context.UrlDatas.Count());
                Assert.Contains(urlData, context.UrlDatas);
            }
        }

        [Fact]
        public async Task DeleteAsync_RemovesUrlDataFromDatabase()
        {
            // Arrange
            var urlData = new UrlData { Id = 1, OriginalUrl = "http://example.com" };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.Add(urlData);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                await repository.DeleteAsync(urlData);

                // Assert
                Assert.Empty(context.UrlDatas);
            }
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllUrlData()
        {
            // Arrange
            var urlDataList = new List<UrlData>
            {
                new UrlData { Id = 1, OriginalUrl = "http://example1.com" },
                new UrlData { Id = 2, OriginalUrl = "http://example2.com" }
            };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.AddRange(urlDataList);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                var result = await repository.GetAllAsync();

                // Assert
                Assert.Equal(urlDataList.Count, result.Count());
                Assert.Contains(urlDataList[0], result);
                Assert.Contains(urlDataList[1], result);
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUrlDataWithMatchingId()
        {
            // Arrange
            var urlDataList = new List<UrlData>
            {
                new UrlData { Id = 1, OriginalUrl = "http://example1.com" },
                new UrlData { Id = 2, OriginalUrl = "http://example2.com" }
            };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.AddRange(urlDataList);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                var result = await repository.GetByIdAsync(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("http://example2.com", result.OriginalUrl);
            }
        }

        [Fact]
        public async Task GetByOriginalUrlAsync_ReturnsUrlDataWithMatchingOriginalUrl()
        {
            // Arrange
            var urlDataList = new List<UrlData>
            {
                new UrlData { Id = 1, OriginalUrl = "http://example1.com" },
                new UrlData { Id = 2, OriginalUrl = "http://example2.com" }
            };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.AddRange(urlDataList);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                var result = await repository.GetByOriginalUrlAsync("http://example2.com");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("http://example2.com", result.OriginalUrl);
            }
        }

        [Fact]
        public async Task GetByShortUrlAsync_ReturnsUrlDataWithMatchingShortUrl()
        {
            // Arrange
            var urlDataList = new List<UrlData>
            {
                new UrlData { Id = 1, OriginalUrl = "http://example1.com", ShortUrl = "abc123" },
                new UrlData { Id = 2, OriginalUrl = "http://example2.com", ShortUrl = "def456" }
            };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.AddRange(urlDataList);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                var result = await repository.GetByShortUrlAsync("def456");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal("http://example2.com", result.OriginalUrl);
                Assert.Equal("def456", result.ShortUrl);
            }
        }

        [Fact]
        public async Task GetUserUrlsAsync_ReturnsUrlDataForMatchingUser()
        {
            // Arrange
            var user = new IdentityUser { Id = "1", UserName = "testuser" };

            var urlDataList = new List<UrlData>
            {
                new UrlData { Id = 1, OriginalUrl = "http://example1.com", User = user },
                new UrlData { Id = 2, OriginalUrl = "http://example2.com", User = user },
                new UrlData { Id = 3, OriginalUrl = "http://example3.com" }
            };

            using (var context = new UrlShortenerDbContext(CreateNewContextOptions()))
            {
                context.UrlDatas.AddRange(urlDataList);
                context.SaveChanges();

                var repository = new UrlDataRepository(context);

                // Act
                var result = await repository.GetUserUrlsAsync(user);

                // Assert
                Assert.Equal(2, result.Count());
                Assert.Contains(urlDataList[0], result);
                Assert.Contains(urlDataList[1], result);
            }
        }
    }
}
