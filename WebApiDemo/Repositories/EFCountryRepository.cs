using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiDemo.Database;
using WebApiDemo.Models;
using Z.EntityFramework.Plus;
using System.Linq;

namespace WebApiDemo.Repositories
{
    public class EFCountryRepository : ICountryRepository
    {
        private readonly DemoDbContext _dbContext;
        public EFCountryRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetAsync()
        {
            return await _dbContext.Country.AsNoTracking().ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            return await _dbContext.Country.AsNoTracking().FirstOrDefaultAsync(x => x.CountryId == countryId);
        }

        public async Task<long> AddAsync(Country country)
        {
            _dbContext.Add(country);
            await _dbContext.SaveChangesAsync();
            return country.CountryId;
        }

        public async Task<int> UpdateAsync(Country country)
        {
            return await _dbContext.Country
                                    .Where(x => x.CountryId == country.CountryId)
                                    .UpdateAsync(x => new Country
                                    {
                                        CountryName = country.CountryName,
                                        Description = country.Description
                                    });
        }

        public async Task<int> UpdateDescriptionAsync(int countryId, string description)
        {
            return await _dbContext.Country
                                    .Where(x => x.CountryId == countryId)
                                    .UpdateAsync(x => new Country
                                    {
                                        Description = description
                                    });

        }

        public async Task<int> DeleteAsync(int countryId)
        {
            return await _dbContext.Country
                                    .Where(x => x.CountryId == countryId)
                                    .DeleteAsync();
        }
    }
}