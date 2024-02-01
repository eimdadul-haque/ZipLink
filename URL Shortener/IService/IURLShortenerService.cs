namespace URL_Shortener.IService
{
    public interface IURLShortenerService
    {
        Task<string> GenerateUniqueCodeAsync();
        Task<string> GenerateShortUrlAsync(string code, string Url);
    }
}
