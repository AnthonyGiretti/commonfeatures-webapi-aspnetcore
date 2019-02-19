using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAsync();
        Task<Country> GetByIdAsync(int countryId);
        Task<long> AddAsync(Country country);
        Task<int> UpdateAsync(int countryId, Country country);
        Task<int> UpdateDescriptionAsync(int countryId, string description);
        Task<int> DeleteAsync(int countryId);
    }
}
