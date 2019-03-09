using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.HttpClients;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoHttpClientController : ControllerBase
    {
        private readonly IDataClient _dataClient;

        public DemoHttpClientController(IDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _dataClient.GetData();
            return Ok(data);
        }
    }
}