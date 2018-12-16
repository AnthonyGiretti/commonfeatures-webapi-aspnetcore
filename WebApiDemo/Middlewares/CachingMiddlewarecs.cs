using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Middlewares
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;

        public CachingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Sample of global cache for any request that returns 200
            context.Response.GetTypedHeaders().CacheControl =
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromSeconds(10),
                Public = true
            };
            var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
            if (responseCachingFeature != null)
            {
                responseCachingFeature.VaryByQueryKeys = new[] { "Param1" };
            }
            await _next(context);
        }     
    }
}
