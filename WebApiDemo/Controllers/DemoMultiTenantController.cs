using Microsoft.AspNetCore.Mvc;
using System;
using WebApiDemo.Services.Tenants;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoMultiTenantController : ControllerBase
    {
        private Func<string, ITenantService> _tenantServiceProvider;

        public DemoMultiTenantController(Func<string, ITenantService> tenantServiceProvider)
        {
            _tenantServiceProvider = tenantServiceProvider;
        }

        // GET: api/DemoMultiTenant/5
        [HttpGet("{tenant}", Name = "Get")]
        public string Get(string tenant)
        {
            return _tenantServiceProvider(tenant).GetName();
        }
    }
}
