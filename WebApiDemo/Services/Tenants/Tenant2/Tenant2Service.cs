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

        public Tenant2Service(IMyRepository myRepository)
        {
            _myRepository = myRepository;
        }

        public string GetName()
        {
            return "Tenant2";
        }
    }
}
