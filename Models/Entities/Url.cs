namespace URL_Shortener.Models.Entities
{
    public class Url
    {
        public Guid Id  { get; set; }
        public string RealUrl  { get; set; }
        public string ShortUrl  { get; set; }
        public string Code  { get; set; }
        public DateTime CreatedOnUtc  { get; set; }
    }
}
