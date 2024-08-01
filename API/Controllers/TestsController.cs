using Amara.Microservice.Shared.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]"), AllowAnonymous]
    public class TestsController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly ICacheService cacheService;

        public TestsController(
            IEmailService emailService,
            ICacheService cacheService)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        [HttpGet("emailService")]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var success = await emailService.SendAsync("subject-test", "hello world", "alvin.espinosa@gmail.com", cancellationToken);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("cacheService")]
        public async Task<IActionResult> GetCacheServiceAsync(CancellationToken cancellationToken)
        {

            var key = "alvinespinosa-101986";
            var cache = await cacheService.GetFromCache<string>(key);

            if (cache == null)
            {
                await cacheService.SetToCache(key, Guid.NewGuid().ToString());
            }

            return Ok(cache);
        }
    }
}
