using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;
using WebApiDemo.RetryPolicies.Config;

namespace WebApiDemo.RetryPolicies
{
    public class HttpCircuitBreakerPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy(ILogger logger, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
        {
            return HttpPolicyBuilders.GetDefaultBuilder()
                                          .CircuitBreakerAsync(circuitBreakerPolicyConfig.RetryCount + 1,
                                                               TimeSpan.FromSeconds(circuitBreakerPolicyConfig.BreakDuration),
                                                               (result, breakDuration) =>
                                                               {
                                                                   OnHttpBreak(result, breakDuration, circuitBreakerPolicyConfig.RetryCount, logger);
                                                               },
                                                               () =>
                                                               {
                                                                   OnHttpReset(logger);
                                                               });
        }

        public static void OnHttpBreak(DelegateResult<HttpResponseMessage> result, TimeSpan breakDuration, int retryCount, ILogger logger)
        {
            logger.LogWarning("Service shutdown during {breakDuration} after {DefaultRetryCount} failed retries.", breakDuration, retryCount);
            throw new BrokenCircuitException("Service inoperative. Please try again later");
        }

        public static void OnHttpReset(ILogger logger)
        {
            logger.LogInformation("Service restarted.");
        }
    }
}
