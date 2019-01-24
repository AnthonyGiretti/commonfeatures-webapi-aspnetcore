using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Services.Tenants.Tenant2
{
    public class Tenant2Service : ITenantService
    {
        public string GetName()
        {
            return "Tenant2";
        }
    }
}
