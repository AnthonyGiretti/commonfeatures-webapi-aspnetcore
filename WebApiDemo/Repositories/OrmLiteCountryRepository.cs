using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;
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

        public async Task<List<Country>> Get()
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return (await connection.SelectAsync<Country>()).ToList();
            }
        }

        public async Task<Country> GetById(int countryId)
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return (await connection.SelectAsync<Country>(x=> x.CountryId == countryId)).FirstOrDefault();
            }
        }

        public async Task<long> Add(Country country)
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return await connection.InsertAsync(country, selectIdentity: true);
            }
        }

        public async Task<int> Update(Country country)
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return await connection.UpdateAsync(country);
            }
        }

        public async Task<int> UpdateDescription(int countryId, string description)
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return await connection.UpdateOnlyAsync(() => new Country
                {
                    Description = "description"
                }, where: p => p.CountryId == countryId);
            }
        }

        public async Task<int> Delete(int countryId)
        {
            using (var connection = _connectionFactory.OpenDbConnection())
            {
                return await connection.DeleteAsync<Country>(x => x.CountryId == countryId);
            }
        }
    }
}
