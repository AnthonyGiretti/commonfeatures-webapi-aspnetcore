using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    /*
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DemoApiVersionningController : ControllerBase
    {
        public DemoApiVersionningController()
        {

        }

        [HttpGet]
        public IActionResult Get1(string version)
        {
            return Ok(HttpContext.GetRequestedApiVersion().ToString());
        }

        [HttpGet, MapToApiVersion("1.1")]
        public IActionResult Get1_1()
        {
            return Ok(HttpContext.GetRequestedApiVersion().ToString());
        }
    }

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DemoApiVersionningV2Controller : ControllerBase
    {
        public DemoApiVersionningV2Controller()
        {

        }

        [HttpGet, MapToApiVersion("2.0")]
        public IActionResult Get2()
        {
            return Ok(HttpContext.GetRequestedApiVersion().ToString());
        }

    }*/
}