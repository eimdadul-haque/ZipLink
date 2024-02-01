using Microsoft.EntityFrameworkCore;
using URL_Shortener.Data;

namespace URL_Shortener.Services
{
    public class URLShortenerService
    {
        public const int maxUrlLenght = 7;
        public const string alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random random = new Random();

        private readonly URLShortenerDbContext _dbContext;
        public URLShortenerService(URLShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateUniqueCode()
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
    }
}
