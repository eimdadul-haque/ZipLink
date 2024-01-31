using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Models.Dtos;

namespace URL_Shortener.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        public UrlShortenerController() { }

        public async Task<IActionResult> Short(UrlShortenerDto request)
        {
            if (Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                return BadRequest("Invaild url!");
            }

            return Ok();
        }
    }
}
