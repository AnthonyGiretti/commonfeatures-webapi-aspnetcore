using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> Get();
        Task<Country> GetById(int countryId);
        Task<long> Add(Country country);
        Task<int> Update(Country country);
        Task<int> UpdateDescription(int countryId, string description);
        Task<int> Delete(int countryId);
    }
}
