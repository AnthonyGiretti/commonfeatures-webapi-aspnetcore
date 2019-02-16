
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Helpers;
using WebApiDemo.HttpClients;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoStreamingController : ControllerBase
    {
        private readonly IStreamingClient _streamingClient;
        public DemoStreamingController(IStreamingClient streamingClient)
        {
            _streamingClient = streamingClient;
        }

        public async Task<FileStreamResult> Get()
        {
            var stream = await _streamingClient.GetStream("earth");
            return new FileStreamResult(stream, "video/mp4");
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            var stream = await _streamingClient.GetStream("earth");
            return File(stream.ToByteArray(), "video/mp4", "earth.mp4");

            /*var file = System.IO.File.ReadAllBytes("files/test.sql");
            return File(file, "application/x-sql", "test.sql");*/
        }
    }
}
 