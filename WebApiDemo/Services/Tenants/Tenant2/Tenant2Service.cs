using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Repositories;

namespace WebApiDemo.Services.Tenants.Tenant2
{
    public class Tenant2Service : ITenantService
    {
        private IMyRepository _myRepository;
        private Guid _guid;

        public Tenant2Service(IMyRepository myRepository)
        {
            _myRepository = myRepository;
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
