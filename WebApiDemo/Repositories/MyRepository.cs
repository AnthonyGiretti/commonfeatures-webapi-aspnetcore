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

        public UserEntity GetUserById(int id)
        {
            // code to get data here.....
            var connectionString = _config.ConnectionString;
            return new UserEntity();
        }
    }
}
