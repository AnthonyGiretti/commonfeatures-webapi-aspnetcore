using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelStateList = context.ModelState.Values.ToList();
                var errors = modelStateList.SelectMany(x => x.Errors.Select(p=> p.ErrorMessage)).ToList();
                throw new ModelValidationException(errors);
            }
        }
    }
}
