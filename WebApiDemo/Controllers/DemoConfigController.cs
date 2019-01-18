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
        // GET: api/DemoConfig
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IMyRepository repository)
        {
            var entity = repository.GetUserById(id);
            return Ok();
        }
    }
}
