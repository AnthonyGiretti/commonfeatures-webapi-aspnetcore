using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Net.Http;
using WebApiDemo.RetryPolicies;
using WebApiDemo.RetryPolicies.Config;

namespace WebApiDemo.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddPolicyHandlers(this IHttpClientBuilder httpClientBuilder, PolicyConfig policyConfig)
        {
            var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
            var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

            return httpClientBuilder.AddRetryPolicyHandler(retryPolicyConfig)
                                    .AddCircuitBreakerHandler(circuitBreakerPolicyConfig);
        }

        public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder httpClientBuilder, IRetryPolicyConfig retryPolicyConfig)
        {
            return httpClientBuilder.AddPolicyHandler((services, request) => HttpRetryPolicies.GetHttpRetryPolicy(services.GetRequiredService<ILogger<IHttpClientBuilder>>(), retryPolicyConfig));
        }

        public static IHttpClientBuilder AddCircuitBreakerHandler(this IHttpClientBuilder httpClientBuilder, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
        {
            return httpClientBuilder.AddPolicyHandler((services, request) => HttpCircuitBreakerPolicies.GetHttpCircuitBreakerPolicy(services.GetRequiredService<ILogger<IHttpClientBuilder>>(), circuitBreakerPolicyConfig));
        }
    }
}
