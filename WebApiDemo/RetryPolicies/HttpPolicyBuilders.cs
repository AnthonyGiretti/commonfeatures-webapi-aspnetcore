using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Net.Http;

namespace WebApiDemo.RetryPolicies
{
    public static class HttpPolicyBuilders
    {
        public static PolicyBuilder<HttpResponseMessage> GetDefaultBuilder()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                                       .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
