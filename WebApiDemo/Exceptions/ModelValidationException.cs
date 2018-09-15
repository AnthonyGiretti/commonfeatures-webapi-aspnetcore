using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo
{
    public class ModelValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public ModelValidationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
