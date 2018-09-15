using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoExceptionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new Exception("bouhhhh vilaine erreur");
            return new string[] { "value1", "value2" };
        }

       
    }
}
