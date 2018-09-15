using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Attributes;
using WebApiDemo.Models;
using WebApiDemo.Validators;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoValidationController : ControllerBase
    {
        [HttpPost]
        [ValidateModel]
        public IActionResult Post(User user)
        {
            return NoContent();
        }
    }
}
