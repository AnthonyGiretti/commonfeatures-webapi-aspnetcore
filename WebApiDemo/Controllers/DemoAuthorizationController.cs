using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

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
            return Ok("Hello");
        }

        // POST api/values
        [Authorize(Policy = "SurveyCreator")]
        [HttpPost]
        public ActionResult Post(User user)
        {
            return Ok();
        }

        // DELETE api/values
        [Authorize(Policy = "SuperSurveyCreator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
