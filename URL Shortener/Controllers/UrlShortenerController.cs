using Microsoft.AspNetCore.Mvc;
using URL_Shortener.IService;
using URL_Shortener.Models.Dtos;

namespace URL_Shortener.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IURLShortenerService _urlShortenerService;
        public UrlShortenerController(
            IURLShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        [HttpPost("Short")]
        public async Task<IActionResult> Short(UrlShortenerDto request)
        {
            if (Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                return BadRequest("Invaild url!");
            }

            string code = await _urlShortenerService
                .GenerateUniqueCodeAsync();

            string shortUrl = await _urlShortenerService
                .GenerateShortUrlAsync(code, request.Url);

            return Ok(shortUrl);
        }
    }
}
