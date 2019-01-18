using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return new UserEntity();
        }
    }
}
