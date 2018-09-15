using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
            .NotNull()
            .WithMessage("FirstName is mandatory.");

            RuleFor(x => x.LastName)
            .NotNull()
            .WithMessage("LastName is mandatory.");

            RuleFor(x => x.SIN)
            .NotNull()
            .WithMessage("SIN is mandatory.")
            .Must((o, list, context) =>
            {
                if (null != o.SIN)
                {
                    context.MessageFormatter.AppendArgument("SIN", o.SIN);
                    return Utilities.IsValidSIN(int.Parse(o.SIN));
                }
                return true;
            })
            .WithMessage("SIN ({SIN}) is not valid.");

            
        }   
    }
}
