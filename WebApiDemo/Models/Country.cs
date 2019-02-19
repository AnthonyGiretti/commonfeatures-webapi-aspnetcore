using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public class Country
    {
        [PrimaryKey]
        [AutoIncrement]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
    }
}
