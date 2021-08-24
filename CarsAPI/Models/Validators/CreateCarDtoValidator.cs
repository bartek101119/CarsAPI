using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Models.Validators
{
    public class CreateCarDtoValidator : AbstractValidator<CreateCarDto>
    {
        public CreateCarDtoValidator()
        {
            RuleFor(x => x.Year)
                .NotEmpty();
            RuleFor(x => x.Model)
                 .NotEmpty();
            RuleFor(x => x.ClientFirstName)
                 .NotEmpty();
            RuleFor(x => x.Surname)
                 .NotEmpty();
            RuleFor(x => x.Cost)
                 .NotEmpty();

            RuleFor(x => x.Cost)
                .Matches("^[0-9]*$");
        }
    }
}
