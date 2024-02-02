using Microsoft.AspNetCore.Mvc;
using URL_Shortener.IService;
using URL_Shortener.Models.Dtos;
using URL_Shortener.Models.Entities;

namespace URL_Shortener.Controllers
{
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

            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out Uri result))
            {
                return BadRequest("Invalid url.");
            }

            string code = await _urlShortenerService
                .GenerateUniqueCodeAsync();

            string shortUrl = await _urlShortenerService
                .GenerateShortUrlAsync(code, request.Url);

            return Ok(shortUrl);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirctToUrl(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }

            string urlString = await _urlShortenerService
                .GetUrlAsync(code);

            if (string.IsNullOrEmpty(urlString))
            {
                return NotFound();
            }

            return Redirect(urlString);
        }
    }
}
