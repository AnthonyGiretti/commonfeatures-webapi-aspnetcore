using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoCachingController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public DemoCachingController(IMemoryCache cache)
        {
            _cache = cache;
        }

        
        // GET: api/DemoCaching/memorycache
        [HttpGet("memorycache")]
        public string Get()
        {
            var cacheEntry = _cache.GetOrCreate("MyCacheKey", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(10);
                return LongTimeOperation();
            });
            return cacheEntry;
        }

        // GET: api/DemoCaching/responsecache
        [HttpGet("responsecache")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]

        public string Get2()
        {
            return LongTimeOperation();
        }

        // GET: api/DemoCaching/globalcache
        [HttpGet("globalcache")]
        public string Get3()
        {
            return LongTimeOperation();
        }

        private string LongTimeOperation()
        {
            Thread.Sleep(5000);
            return "Long time operation done!" + DateTime.Now.ToLongDateString();
        }
    }
}
