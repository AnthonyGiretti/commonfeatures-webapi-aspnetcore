using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo
{
    public class ModelValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }
        public string Code { get; set; }

        public ModelValidationException(IEnumerable<string> errors) : base("Validation errors")
        {
            Errors = errors;
            Code = "00001";
        }
    }
}
