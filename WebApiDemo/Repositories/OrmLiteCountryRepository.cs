using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Services;

namespace WebApiDemo.Repositories
{
    public class OrmLiteCountryRepository : ICountryRepository
    {
        private OrmLiteConnectionFactory _connectionFactory;
        public OrmLiteCountryRepository(IConfig config)
        {
            _connectionFactory = new OrmLiteConnectionFactory(config.ConnectionString, SqlServer2014Dialect.Provider);
        }
    }
}
