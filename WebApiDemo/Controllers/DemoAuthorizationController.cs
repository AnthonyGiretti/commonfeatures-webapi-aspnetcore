using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoAuthorizationController : ControllerBase
    {
        // GET api/values/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Hello";
        }

        // POST api/values
        [Authorize(Policy = "SurveyCreator")]
        //[Authorize(Roles = "SurveyCreator")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/values
        [Authorize(Policy = "SuperSurveyCreator")]
        [HttpDelete]
        public void Delete(int id)
        {
        }

    }
}
