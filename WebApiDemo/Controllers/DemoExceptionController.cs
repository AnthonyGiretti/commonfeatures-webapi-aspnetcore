using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoExceptionController : ControllerBase
    {
        private readonly ILogger<DemoExceptionController> _logger;

        public DemoExceptionController(ILogger<DemoExceptionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                _logger.LogInformation("Could break here :(");
                throw new Exception("bohhhh very bad error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "It broke :(");
                //row e; // let middleware hanle it
            }
            return new string[] { "value1", "value2" };
        }

       
    }
}
