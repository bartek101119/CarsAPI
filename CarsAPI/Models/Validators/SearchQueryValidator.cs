using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsAPI.Models.Validators
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        public int[] allowedPageSizes = new[] { 5, 10, 15 };
        public SearchQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).Custom((value, context) => 
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page Size must in [{string.Join(",", allowedPageSizes)}]");
                }
            
            });
        }
    }
}
