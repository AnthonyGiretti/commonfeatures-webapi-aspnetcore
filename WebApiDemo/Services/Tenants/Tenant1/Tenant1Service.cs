using WebApiDemo.Repositories;

namespace WebApiDemo.Services.Tenants.Tenant1
{
    public class Tenant1Service : ITenantService
    {
        private IMyRepository _myRepository;

        public Tenant1Service(IMyRepository myRepository)
        {
            _myRepository = myRepository;
        }

        public string GetName()
        {
            return "Tenant1";
        }
    }
}
