using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Services.Tenants.Tenant1
{
    public class Tenant1Service : ITenantService
    {
        public string GetName()
        {
            return "Tenant1";
        }
    }
}
