using WebApiDemo.Models;
using WebApiDemo.Services;

namespace WebApiDemo.Repositories
{
    public class MyRepository : IMyRepository
    {
        private IConfig _config;

        public MyRepository(IConfig config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            return _config.ConnectionString;
        }
    }
}