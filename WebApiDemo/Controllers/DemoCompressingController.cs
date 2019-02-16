using System;
using System.Collections.Generic;
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
    public class DemoCompressingController : ControllerBase
    {
        private readonly IStreamingClient _streamingClient;
        public DemoCompressingController(IStreamingClient streamingClient)
        {
            _streamingClient = streamingClient;
        }

        [HttpGet("downloadmp4")]
        public async Task<IActionResult> DownloadMp4()
        {
            var stream = await _streamingClient.GetStream("earth");
            return File(stream.ToByteArray(), "video/mp4", "earth.mp4");
        }

        [HttpGet("downloadsql")]
        public async Task<IActionResult> DownloadSql()
        {
            var file = await Task.Run(()=> System.IO.File.ReadAllBytes("files/test.sql"));
            return File(file, "application/x-sql", "test.sql");
        }
    }
}