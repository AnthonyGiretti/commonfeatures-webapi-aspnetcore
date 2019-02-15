using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.RetryPolicies.Config
{
    public interface IRetryPolicyConfig
    {
        int RetryCount { get; set; }
    }
}
