using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;
using WebApiDemo.RetryPolicies.Config;

namespace WebApiDemo.RetryPolicies
{
    public static class HttpRetryPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy(ILogger logger, IRetryPolicyConfig retryPolicyConfig)
        {
            return HttpPolicyBuilders.GetDefaultBuilder()
                                          .WaitAndRetryAsync(retryPolicyConfig.RetryCount,
                                                             ComputeDuration,
                                                             (result, timeSpan, retryCount, context) =>
                                                             {
                                                                 OnHttpRetry(result, timeSpan, retryCount, context, logger);
                                                             });
        }

        private static void OnHttpRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Polly.Context context, ILogger logger)
        {
            if (result.Result != null)
            {
                logger.LogWarning("Request failed with {StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}", result.Result.StatusCode, timeSpan, retryCount);
            }
            else
            {
                logger.LogWarning("Request failed because network failure. Waiting {timeSpan} before next retry. Retry attempt {retryCount}", timeSpan, retryCount);
            }
        }

        private static TimeSpan ComputeDuration(int input)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, input)) + TimeSpan.FromMilliseconds(new Random().Next(0, 100));
        }
    }
}
