using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;
using WebApiDemo.Services;

namespace WebApiDemo.Repositories
{
    public class OrmLiteCountryRepository : ICountryRepository, IDisposable
    {
        private IDbConnection _connection;
        public OrmLiteCountryRepository(IConfig config)
        {
            _connection = new OrmLiteConnectionFactory(config.ConnectionString, SqlServer2014Dialect.Provider).OpenDbConnection();
        }

        public async Task<List<Country>> Get()
        {
            return (await _connection.SelectAsync<Country>()).ToList();
        }

        public async Task<Country> GetById(int countryId)
        {
            return (await _connection.SelectAsync<Country>(x=> x.CountryId == countryId)).FirstOrDefault();
        }

        public async Task<long> Add(Country country)
        {
            return await _connection.InsertAsync(country, selectIdentity: true);
        }

        public async Task<int> Update(Country country)
        {
            return await _connection.UpdateAsync(country);
        }

        public async Task<int> UpdateDescription(int countryId, string description)
        {
            return await _connection.UpdateOnlyAsync(() => new Country
            {
                Description = "description"
            }, where: p => p.CountryId == countryId);
            
        }

        public async Task<int> Delete(int countryId)
        {
            return await _connection.DeleteAsync<Country>(x => x.CountryId == countryId);
        }

        public void Dispose()
        {
            if (null != _connection)
                _connection.Dispose();
        }
    }
}
