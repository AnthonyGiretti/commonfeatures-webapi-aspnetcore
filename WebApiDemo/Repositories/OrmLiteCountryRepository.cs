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
            _connection = new OrmLiteConnectionFactory(config.ConnectionString, SqlServer2016Dialect.Provider).OpenDbConnection();
        }

        public async Task<List<Country>> GetAsync()
        {
            return (await _connection.SelectAsync<Country>()).ToList();
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            return (await _connection.SelectAsync<Country>(x=> x.CountryId == countryId)).FirstOrDefault();
        }

        public async Task<long> AddAsync(Country country)
        {
            return await _connection.InsertAsync(country, selectIdentity: true);
        }

        public async Task<int> UpdateAsync(int countryId, Country country)
        {
            country.CountryId = countryId;
            return await _connection.UpdateAsync(country);
        }

        public async Task<int> UpdateDescriptionAsync(int countryId, string description)
        {
            return await _connection.UpdateOnlyAsync(() => new Country
            {
                Description = description
            }, where: p => p.CountryId == countryId);
            
        }

        public async Task<int> DeleteAsync(int countryId)
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
