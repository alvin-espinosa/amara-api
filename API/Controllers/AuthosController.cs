

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        public AuthorsController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
