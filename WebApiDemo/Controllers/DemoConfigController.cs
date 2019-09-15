using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApiDemo.Models;
using WebApiDemo.Repositories;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoConfigController : ControllerBase
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public DemoConfigController(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IMyRepository repository)
        {
            return Ok(repository.GetConnectionString());
        }
    }
}