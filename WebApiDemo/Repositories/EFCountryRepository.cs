using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Database;

namespace WebApiDemo.Repositories
{
    public class EFCountryRepository : ICountryRepository
    {
        private readonly DemoDbContext _dbContext;
        public EFCountryRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}