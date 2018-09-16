using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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
            string url1 = string.Empty;
            string url2 = string.Empty;

            using (MiniProfiler.Current.Step("Get method"))
            {
                using (MiniProfiler.Current.Step("Prepare data"))
                {
                    // Could be a SQL call
                    Thread.Sleep(500);
                    url1 = "https://google.com";
                    url2 = "https://stackoverflow.com/";
                }
                using (MiniProfiler.Current.Step("Use data for http call"))
                {
                    using (MiniProfiler.Current.CustomTiming("http", "GET " + url1))
                    {
                        var client = new WebClient();
                        var reply = client.DownloadString(url1);
                    }

                    using (MiniProfiler.Current.CustomTiming("http", "GET " + url2))
                    {
                        var client = new WebClient();
                        var reply = client.DownloadString(url2);
                    }
                }
                
            }
            return new string[] { "value1", "value2" };        
        }

    }
}
