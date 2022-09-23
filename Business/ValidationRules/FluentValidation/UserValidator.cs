using Core.Entities.Concrete;
using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).EmailAddress().NotEmpty();
            
        }

        private bool StartWithUpper(string arg)
        {
            return arg.StartsWith("A".ToUpper());
        }
    }
}
