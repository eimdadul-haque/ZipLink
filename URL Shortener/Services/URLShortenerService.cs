namespace URL_Shortener.Services
{
    public class URLShortenerService
    {
        public int maxUrlLenght = 7;
        public const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random random = new Random();

        public async Task<string> GenerateUniqueCode()
        {

            return string.Empty;
        }
    }
}
