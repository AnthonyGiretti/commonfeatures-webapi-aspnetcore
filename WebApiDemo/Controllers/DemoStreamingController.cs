using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
 