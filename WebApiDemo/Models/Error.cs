using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class Error2
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
