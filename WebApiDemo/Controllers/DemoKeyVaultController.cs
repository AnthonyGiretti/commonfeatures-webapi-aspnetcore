using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoKeyVaultController : ControllerBase
    {
        private IConfiguration _configuration { get; set; }
        public DemoKeyVaultController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Get()
        {
            var connectionString = _configuration.GetValue<string>("MySecretConnectionString");
            return Ok(connectionString);
        }
    }
}