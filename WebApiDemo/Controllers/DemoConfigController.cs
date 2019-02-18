using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Repositories;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoConfigController : ControllerBase
    {
        public IActionResult Get([FromServices] ICountryRepository repository)
        {
            return Ok();
        }
    }
}