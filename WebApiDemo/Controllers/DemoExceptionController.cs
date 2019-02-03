using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoExceptionController : ControllerBase
    {
        public DemoExceptionController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new Exception("bouhhhh quelle affreuse erreur!");
            return new string[] { "value1", "value2" };
        }

    }
}
