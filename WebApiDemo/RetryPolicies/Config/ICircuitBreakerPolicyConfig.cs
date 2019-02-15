using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.RetryPolicies.Config
{
    public interface ICircuitBreakerPolicyConfig
    {
        int RetryCount { get; set; }
        int BreakDuration { get; set; }
    }
}
