using System;
using WebApiDemo.Repositories;

namespace WebApiDemo.Services.Tenants.Tenant2
{
    public class Tenant2Service : ITenantService
    {
        private Guid _guid;

        public Tenant2Service()
        {
            _guid = Guid.NewGuid();
        }

        public string GetName()
        {
            return "Tenant2, instance id : " + GetGuid();
        }

        private string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
