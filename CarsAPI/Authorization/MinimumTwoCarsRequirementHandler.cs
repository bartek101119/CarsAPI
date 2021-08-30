using CarsAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarsAPI.Authorization
{
    public class MinimumTwoCarsRequirementHandler : AuthorizationHandler<MinimumTwoCarsRequirement>
    {
        private readonly CarDbContext carDbContext;

        public MinimumTwoCarsRequirementHandler(CarDbContext carDbContext)
        {
            this.carDbContext = carDbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumTwoCarsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var carsCount = carDbContext.Cars.Count(c => c.CreateById == userId);

            if(carsCount > 2)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
