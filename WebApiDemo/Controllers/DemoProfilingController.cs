using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoProfilingController : ControllerBase
    {
        // GET: api/DemoProfiling
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var profiler = MiniProfiler.StartNew("Get method");
            using (profiler.Step("Get Work"))
            {
                return new string[] { "value1", "value2" };
            }
            
        }

    }
}
