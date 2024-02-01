using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;
using URL_Shortener.IService;
using URL_Shortener.Models.Entities;

namespace URL_Shortener.Services
{
    public class URLShortenerService : IURLShortenerService
    {
        public const string alphabets = 
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public const int maxUrlLenght = 7;
        private readonly Random random = new Random();
        private readonly URLShortenerDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public URLShortenerService(
            URLShortenerDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GenerateUniqueCodeAsync()
        {
            while (true)
            {
                var codeChars = new char[maxUrlLenght];

                for (int i = 0; i < codeChars.Length; i++)
                {
                    int randomIndex = random
                        .Next(0, codeChars.Length - 1);

                    codeChars[i] = alphabets[randomIndex];
                }

                string code = new string(codeChars);

                var isExist = await _dbContext.Urls
                    .AnyAsync(x => x.Code == code);

                if (!isExist)
                {
                    return code;
                }
            }
        }

        public async Task<string> GenerateShortUrlAsync(string code, string Url)
        {
            var request = _httpContextAccessor?
                .HttpContext?
                .Request;

            var url = new Url()
            {
                Id = Guid.NewGuid(),
                CreatedOnUtc = DateTime.UtcNow,
                RealUrl = Url,
                Code = code,
                ShortUrl = $"{request?.Scheme}://{request?.Host}/{code}"
            };
            
            await _dbContext.Urls.AddAsync(url);
            int row = await _dbContext.SaveChangesAsync();
            if(row > 0)
            {
                throw new Exception("Error in insertion.");
            }

            return url.ShortUrl;
        }
    }
}
