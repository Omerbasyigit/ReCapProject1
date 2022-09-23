using Entity.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator:AbstractValidator<UserForRegisterDto>
    {
        public AuthValidator()
        {
            RuleFor(u => u.FirstName).MaximumLength(50);
            RuleFor(u => u.FirstName).MinimumLength(3);
            RuleFor(u => u.LastName).MaximumLength(50);
            RuleFor(u => u.LastName).MinimumLength(2);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u=>u.Password).MinimumLength(5);
            RuleFor(U => U.Password).NotEmpty();
            RuleFor(U => U.FirstName).NotEmpty();
            RuleFor(U => U.LastName).NotEmpty();
            RuleFor(U => U.Email).NotEmpty();
        }
    }
}
