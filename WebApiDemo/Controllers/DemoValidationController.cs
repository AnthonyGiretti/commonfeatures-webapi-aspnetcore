using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiDemo.Models;
using WebApiDemo.Validators;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoValidationController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(User user)
        {
            return NoContent();
        }
    }
}
