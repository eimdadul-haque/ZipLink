namespace URL_Shortener.IService
{
    public interface IURLShortenerService
    {
        Task<string> GenerateUniqueCode();
    }
}
