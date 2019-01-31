using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Providers;
using WebApiDemo.Services.Tenants;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoMultiTenantController : ControllerBase
    {
        private IServicesProvider<ITenantService> _tenantServiceProvider;

        public DemoMultiTenantController(IServicesProvider<ITenantService> tenantServiceProvider)
        {
            _tenantServiceProvider = tenantServiceProvider;
        }

        // GET: api/DemoMultiTenant/Tenant1
        // GET: api/DemoMultiTenant/Tenant2
        [HttpGet("{tenant}", Name = "Get")]
        public string Get(string tenant)
        {
            return _tenantServiceProvider.GetInstance(tenant).GetName();
        }
    }
}