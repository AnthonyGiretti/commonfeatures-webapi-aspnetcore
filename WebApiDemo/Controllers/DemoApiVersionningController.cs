using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/{version:apiVersion}/home")]
    [ApiController]
    public class DemoApiVersionningController : ControllerBase
    {
        public DemoApiVersionningController()
        {

        }

        [HttpGet, MapToApiVersion("1.0")]
        public IActionResult Get1()
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
    [Route("api/{version:apiVersion}/home")]
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

    }
}