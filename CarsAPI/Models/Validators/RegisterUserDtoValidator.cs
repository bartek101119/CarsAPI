using CarsAPI.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(CarDbContext dbContext)
        {
            RuleFor(x => x.Email).Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) => 
                {
                    var email = dbContext.Users.Any(x => x.Email == value);
                    if (email)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
            RuleFor(x => x.Login)
                .Custom((value, context) => 
                {
                    var login = dbContext.Users.Any(x => x.Login == value);
                    if (login)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }
                }
                );
        }
    }
}
