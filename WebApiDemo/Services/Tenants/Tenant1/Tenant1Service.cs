using System;
using WebApiDemo.Repositories;

namespace WebApiDemo.Services.Tenants.Tenant1
{
    public class Tenant1Service : ITenantService
    {
        private IMyRepository _myRepository;
        private Guid _guid;

        public Tenant1Service(IMyRepository myRepository)
        {
            _myRepository = myRepository;
            _guid = Guid.NewGuid();
        }

        public string GetName()
        {
            return "Tenant1, instance id : " + GetGuid();
        }

        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
