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
        private static Func<IServiceProvider, ILogger<IHttpClientBuilder>> LoggerFunc = (services) => services.GetRequiredService<ILogger<IHttpClientBuilder>>();

        public static IHttpClientBuilder AddPolicyHandlers(this IHttpClientBuilder httpClientBuilder, PolicyConfig policyConfig)
        {
            var circuitBreakerPolicyConfig = (ICircuitBreakerPolicyConfig)policyConfig;
            var retryPolicyConfig = (IRetryPolicyConfig)policyConfig;

            return httpClientBuilder.AddRetryPolicyHandler(retryPolicyConfig)
                                    .AddCircuitBreakerHandler(circuitBreakerPolicyConfig);
        }

        public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder httpClientBuilder, IRetryPolicyConfig retryPolicyConfig)
        {
            Func<IServiceProvider, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> myFunc = (services, request) => HttpRetryPolicies.GetHttpRetryPolicy(LoggerFunc(services), retryPolicyConfig);

            return httpClientBuilder.AddPolicyHandler(myFunc);
        }

        public static IHttpClientBuilder AddCircuitBreakerHandler(this IHttpClientBuilder httpClientBuilder, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
        {
            Func<IServiceProvider, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> myFunc = (services, request) => HttpCircuitBreakerPolicies.GetHttpCircuitBreakerPolicy(LoggerFunc(services), circuitBreakerPolicyConfig);

            return httpClientBuilder.AddPolicyHandler(myFunc);
        }
    }
}
