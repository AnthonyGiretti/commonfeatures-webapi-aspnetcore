using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Enum;

namespace WebApiDemo.Models
{
    public class UserEntity
    {
        public string Id { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
